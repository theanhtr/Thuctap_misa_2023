using AutoMapper;
using Microsoft.AspNetCore.Http;
using MISA.WebFresher052023.CTM.Domain;

namespace MISA.WebFresher052023.CTM.Application
{
    public class ExcelService<TEntity, TEntityDto, TEntityCreateDto> : IExcelService<TEntity, TEntityDto, TEntityCreateDto>
    {
        #region Fields
        protected readonly IBaseRepository<TEntity> _baseRepository;
        protected readonly IMapper _mapper;
        protected readonly IBaseValidate<TEntity> _baseValidate;
        protected readonly IExcelWorker<TEntityDto, TEntityCreateDto> _excelWorker;
        private readonly string tableName;
        #endregion

        #region Constructor
        public ExcelService(IBaseRepository<TEntity> baseRepository, IMapper mapper, IBaseValidate<TEntity> baseValidate, IExcelWorker<TEntityDto, TEntityCreateDto> excelWorker)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
            _baseValidate = baseValidate;
            tableName = typeof(TEntity).Name;
            _excelWorker = excelWorker;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Export dữ liệu ra file excel (xlsx)
        /// </summary>
        /// <param name="searchText">Nội dung tìm kiếm</param>
        /// <returns>Dữ liệu trong file excel</returns>
        /// CreatedBy: TTANH (16/07/2023)
        public async Task<byte[]> ExportToExcelAsync(string? searchText)
        {
            var entities = await _baseRepository.GetAsync();

            if (searchText != null)
            {
                entities = await _baseRepository.FilterAsync(999999, 1, searchText);
            }

            var entitiesDto = _mapper.Map<List<TEntityDto>>(entities);

            var content = _excelWorker.ConvertDataToExcelData(entitiesDto);
            
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
            var importExcelSettingData = _excelWorker.GetExcelSettingData(filePath, headerFind);

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
            var headersInfo = _excelWorker.GetHeadersInfo(filePath, SheetContainsData, HeaderRowIndex);

            return headersInfo;
        }
        #endregion
    }
}
