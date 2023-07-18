﻿using MISA.WebFresher052023.CTM.Domain;

namespace MISA.WebFresher052023.CTM.Application
{
    public interface IExcelWorker<TEntityDto, TEntityCreateDto>
    {
        /// <summary>
        /// hàm chuyển đổi dữ liệu đầu vào từ entities thành dữ liệu excel
        /// </summary>
        /// <param name="entities">Các đối tượng cần chuyển</param>
        /// <returns>Dữ liệu excel</returns>
        /// CreatedBy: TTANH (17/07/2023)
        byte[] ConvertDataToExcelData(List<TEntityDto> entitiesDto);

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
    }
}