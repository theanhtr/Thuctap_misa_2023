using MISA.WebFresher052023.CTM.Domain;

namespace MISA.WebFresher052023.CTM.Application
{
    /// <summary>
    /// Interface để làm việc với excel
    /// </summary>
    /// CreatedBy: TTANH (16/07/2023)
    public interface IExcelService<TEntity, TEntityDto, TEntityCreateDto>
    {
        #region Methods
        /// <summary>
        /// Export dữ liệu ra file excel (xlsx)
        /// </summary>
        /// <param name="searchText">Nội dung tìm kiếm</param>
        /// <returns>Dữ liệu trong file excel</returns>
        /// CreatedBy: TTANH (16/07/2023)
        Task<byte[]> ExportToExcelAsync(string? searchText);

        /// <summary>
        /// Đọc file excel để lấy dữ liệu cho setting
        /// </summary>
        /// <param name="filePath">đường dẫn tới file</param>
        /// <param name="headerFind">cột để xác định headerRowIndex</param>
        /// CreatedBy: TTANH (17/07/2023)
        ExcelImportSettingData GetExcelSettingData(string filePath, string headerFind);

        /// <summary>
        /// Lấy ra các thông tin của các tiêu đề
        /// </summary>
        /// <param name="filePath">đường dẫn tới file</param>
        /// <param name="SheetContainsData">sheet tương tác</param>
        /// <param name="HeaderRowIndex">vị trí của tiêu đề</param>
        /// <returns>thông tin các cột trong sheet</returns>
        /// CreatedBy: TTANH (17/07/2023)
        List<ExcelHeadersInfo> GetHeadersInfo(string filePath, string SheetContainsData, int HeaderRowIndex);
        #endregion
    }
}
