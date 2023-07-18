using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher052023.CTM.Application;
using MISA.WebFresher052023.CTM.Domain;

namespace MISA.WebFresher052023.CTM.Controllers
{
    /// <summary>
    /// Controller của nhân viên
    /// </summary>
    /// CreatedBy: TTANH (12/07/2023)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeDto, EmployeeCreateDto, EmployeeUpdateDto>
    {
        #region Fields
        private readonly IExcelService<Employee, EmployeeDto, EmployeeCreateDto> _excelService;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly string folderStore;
        #endregion

        #region Constructor
        public EmployeesController(IEmployeeService employeeService, IExcelService<Employee, EmployeeDto, EmployeeCreateDto> excelService, IHostEnvironment hostEnvironment) : base(employeeService)
        {
            _excelService = excelService;
            _hostEnvironment = hostEnvironment;
            folderStore = Path.Combine(_hostEnvironment.ContentRootPath, "employee-files");
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hàm xuất dữ liệu ra file excel
        /// </summary>
        /// <param name="searchText">Nội dung tìm kiếm</param>
        /// <returns>Download file</returns>
        /// CreatedBy: TTANH (16/07/2023)
        [HttpGet("excel")]
        public async Task<IActionResult> ExportToExcelAsync(string? searchText)
        {
            var content = await _excelService.ExportToExcelAsync(searchText);
                
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Danh_sach_nhan_vien.xlsx");
        }

        /// <summary>
        /// Hàm import file excel
        /// </summary>
        /// CreatedBy: TTANH (17/07/2023)
        [HttpPost("excel/import")]
        public async Task<IActionResult> ImportExcelAsync(IFormFile excelFile)
        {
            if (excelFile != null && excelFile.Length > 0)
            {
                HttpContext.Session.SetString("ExcelFileId", Guid.NewGuid().ToString());

                var excelFileId = HttpContext.Session.GetString("ExcelFileId");

                string fileName = $"{excelFileId}.xlsx";
                string filePath = Path.Combine(this.folderStore, fileName);

                if (!Directory.Exists(this.folderStore))
                {
                    Directory.CreateDirectory(this.folderStore);
                }

                using var fileStream = System.IO.File.Create(filePath);

                excelFile.CopyTo(fileStream);

                fileStream.Close();

                var dataSetting = _excelService.GetExcelSettingData(filePath, ResourceVN.Excel_Employee_Code_Header_Name);

                //// Xóa tệp Excel tạm thời sau khi đã xử lý xong
                //System.IO.File.Delete(filePath);

                return StatusCode(StatusCodes.Status200OK, dataSetting);
            } 
            else
            {
                throw new ValidateException();
            }

        }
        
        [HttpPost("excel/add-setting")]
        public async Task<IActionResult> AddSettingExcelAsync([FromBody] ExcelImportSetting importExcelSetting)
        {
            HttpContext.Session.SetString("SheetContainsData", importExcelSetting.SheetContainsData.ToString());
            HttpContext.Session.SetString("HeaderRowIndex", importExcelSetting.HeaderRowIndex.ToString());
            HttpContext.Session.SetString("ImportMode", importExcelSetting.ImportMode.ToString());

            var excelFileId = HttpContext.Session.GetString("ExcelFileId");

            string fileName = $"{excelFileId}.xlsx";
            string filePath = Path.Combine(this.folderStore, fileName);

            var headersInfo = _excelService.GetHeadersInfo(filePath, importExcelSetting.SheetContainsData, importExcelSetting.HeaderRowIndex);

            return StatusCode(StatusCodes.Status200OK, headersInfo);
        }
        #endregion
    }
}
