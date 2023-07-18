using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using MISA.WebFresher052023.CTM.Domain;

namespace MISA.WebFresher052023.CTM.Application
{
    public abstract class ClosedXMLExcelWorker<TEntityDto, TEntityCreateDto> : IExcelWorker<TEntityDto, TEntityCreateDto>
    {
        #region Methods
        /// <summary>
        /// hàm chuyển đổi dữ liệu đầu vào từ entities thành dữ liệu excel
        /// </summary>
        /// <param name="entities">Các đối tượng cần chuyển</param>
        /// <returns>Dữ liệu excel</returns>
        /// CreatedBy: TTANH (17/07/2023)
        public byte[] ConvertDataToExcelData(List<TEntityDto> entitiesDto)
        {
            using var workbook = new XLWorkbook();

            var worksheet = workbook.Worksheets.Add("Danh_sach");
            var currentRow = 1;
            var currentColumn = 1;

            var type = typeof(TEntityDto);
            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                var propertyName = property.Name;

                if (!propertyName.Contains("Id"))
                {
                    worksheet.Cell(currentRow, currentColumn).Value = propertyName;

                    currentColumn++;
                }
            }

            currentColumn = 1;

            foreach (var entity in entitiesDto)
            {
                currentRow++;

                foreach (var property in properties)
                {
                    var propertyName = property.Name;

                    if (!propertyName.Contains("Id"))
                    {
                        var propertyValue = property.GetValue(entity);

                        worksheet.Cell(currentRow, currentColumn).Value = propertyValue?.ToString();

                        currentColumn++;
                    }
                }

                currentColumn = 1;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            return content;
        }

        /// <summary>
        /// Đọc file excel để lấy dữ liệu cho setting
        /// </summary>
        /// <param name="filePath">đường dẫn tới file</param>
        /// <param name="headerFind">cột để xác định headerRowIndex</param>
        /// CreatedBy: TTANH (17/07/2023)
        public ExcelImportSettingData GetExcelSettingData(string filePath, string headerFind)
        {
            using var workbook = new XLWorkbook(filePath);

            var importExcelSettingData = new ExcelImportSettingData();
            importExcelSettingData.Sheets = new List<string> { };
            importExcelSettingData.HeaderRowIndex = 1;

            foreach (IXLWorksheet worksheet in workbook.Worksheets)
            {
                string sheetName = worksheet.Name;
                importExcelSettingData.Sheets.Add(sheetName);
            }

            var firstWorksheet = workbook.Worksheet(importExcelSettingData.Sheets[0]);
            int lastColumnIndex = firstWorksheet.LastColumnUsed().ColumnNumber();
            int lastRowIndex = firstWorksheet.LastRowUsed().RowNumber();

            for (var r = 1; r <= lastRowIndex; r++)
            {
                var row = firstWorksheet.Row(r);
                var isFindCodeNameHeader = false;

                for (var c = 1; c <= lastColumnIndex; c++)
                {
                    var cell = row.Cell(c);
                    string cellName = cell.GetValue<string>();

                    if (cellName == headerFind)
                    {
                        importExcelSettingData.HeaderRowIndex = r;
                        isFindCodeNameHeader = true;
                        break;
                    }
                }

                if (isFindCodeNameHeader)
                {
                    break;
                }
            }

            return importExcelSettingData;
        }

        /// <summary>
        /// Lấy ra các thông tin của các tiêu đề
        /// </summary>
        /// <param name="filePath">đường dẫn tới file</param>
        /// <param name="SheetContainsData">sheet tương tác</param>
        /// <param name="HeaderRowIndex">vị trí của tiêu đề</param>
        /// <returns>thông tin các cột trong sheet</returns>
        /// CreatedBy: TTANH (17/07/2023)
        public List<ExcelHeadersInfo> GetHeadersInfo(string filePath, string SheetContainsData, int HeaderRowIndex)
        {
            using var workbook = new XLWorkbook(filePath);
            var worksheet = workbook.Worksheet(SheetContainsData);
            
            var columnIndex = 1;
            var headerRow = worksheet.Row(HeaderRowIndex);
            int lastColumnIndex = worksheet.LastColumnUsed().ColumnNumber();

            var headersInfo = new List<ExcelHeadersInfo> { };

            for (int colIndex = columnIndex; colIndex <= lastColumnIndex; colIndex++)
            {
                var cell = headerRow.Cell(colIndex);
                string headerName = cell.GetValue<string>();
                headersInfo.Add(new ExcelHeadersInfo(colIndex, headerName));
            }

            return headersInfo;
        }
        #endregion
    }
}
