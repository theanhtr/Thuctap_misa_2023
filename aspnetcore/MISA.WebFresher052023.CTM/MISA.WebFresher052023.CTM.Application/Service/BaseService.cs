using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using MISA.WebFresher052023.CTM.Domain;
using System.Reflection;

namespace MISA.WebFresher052023.CTM.Application
{
    public abstract class BaseService<TEntity, TEntityDto, TEntityCreateDto, TEntityUpdateDto> : IBaseService<TEntity, TEntityDto, TEntityCreateDto, TEntityUpdateDto>
    {
        #region Fields
        protected readonly IBaseRepository<TEntity> _baseRepository;
        protected readonly IMapper _mapper;
        protected readonly IBaseValidate<TEntity> _baseValidate;
        private readonly string tableName;
        #endregion

        #region Constructor
        public BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper, IBaseValidate<TEntity> baseValidate)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
            _baseValidate = baseValidate;
            tableName = typeof(TEntity).Name;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hàm lấy tất cả bản ghi
        /// </summary>
        /// <returns>Mảng chứa các bản ghi</returns>
        /// Created by: TTANH (15/07/2023)
        public async Task<IEnumerable<TEntityDto>?> GetAsync()
        {
            var entities = await _baseRepository.GetAsync();

            if (entities == null)
            {
                throw new NotFoundException();
            }

            var entitiesDto = _mapper.Map<List<TEntityDto>>(entities);

            return entitiesDto;
        }

        /// <summary>
        /// Hàm lấy một bản ghi theo id
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>Bản ghi</returns>
        /// Created by: TTANH (15/07/2023)
        public async Task<TEntityDto> GetByIdAsync(Guid id)
        {
            var entity = await _baseRepository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new NotFoundException();
            }

            var entityDto = _mapper.Map<TEntityDto>(entity);

            return entityDto;
        }

        /// <summary>
        /// Hàm lấy một bản ghi theo mã code
        /// </summary>
        /// <param name="code">Mã code của bản ghi</param>
        /// <returns>Bản ghi</returns>
        /// Created by: TTANH (15/07/2023)
        public async Task<TEntityDto> GetByCodeAsync(string code)
        {
            var entity = await _baseRepository.GetByCodeAsync(code);

            if (entity == null)
            {
                throw new NotFoundException();
            }

            var entityDto = _mapper.Map<TEntityDto>(entity);

            return entityDto;
        }

        /// <summary>
        /// Hàm lọc dữ liệu bản ghi
        /// </summary>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="pageNumber">Thứ tự bao nhiêu</param>
        /// <param name="searchText">Chuỗi tìm kiếm</param>
        /// <returns>Các bản ghi lọc theo các tiêu chí trên</returns>
        /// Created by: TTANH (15/07/2023)
        public async Task<BaseFilterResponse<TEntityDto>> FilterAsync(int? pageSize, int? pageNumber, string? searchText)
        {
            if (pageSize == null || pageSize < 0) { pageSize = 999999; }
            if (pageNumber == null || pageNumber < 1) { pageNumber = 1; }

            var entitiesNotPagging = await _baseRepository.FilterAsync(int.MaxValue, 1, searchText);

            var totalRecord = entitiesNotPagging.Count();
            var totalPage = Convert.ToInt32(Math.Ceiling((double)totalRecord / (double)pageSize));

            if (pageNumber > totalPage)
            {
                pageNumber = totalPage;
            }

            var entities = await _baseRepository.FilterAsync(pageSize, pageNumber, searchText);

            if (entities == null)
            {
                throw new NotFoundException();
            }

            var currentPage = pageNumber;
            var currentPageRecords = entities.Count();

            var entitiesDto = _mapper.Map<List<TEntityDto>>(entities);

            var filterData = new BaseFilterResponse<TEntityDto>(totalPage, totalRecord, currentPage, currentPageRecords, entitiesDto);

            return filterData;
        }

        /// <summary>
        /// Hàm lấy một mã code mới không trùng
        /// </summary>
        /// <returns>Mã code mới</returns>
        /// Created by: TTANH (15/07/2023)
        public async Task<string> GetNewCodeAsync()
        {
            var lastEntity = await _baseRepository.GetLastInsertAsync();

            if (lastEntity == null)
            {
                return "NV-1001";
            }

            var lastEntityCode = lastEntity.GetType().GetProperty($"{tableName}Code").GetValue(lastEntity).ToString();

            //duyệt từ cuối mảng để lấy ra vị trí bên phải không phải số
            var indexRightNotNumber = 0;

            for (int i = lastEntityCode.Length - 1; i >= 0; i--)
            {
                char c = lastEntityCode[i];
                if (c < '0' || c > '9')
                {
                    indexRightNotNumber = i + 1;
                    break;
                }
            }

            //Biến dùng để lưu template chung của lastEntityCode
            //biến này sẽ là biến lưu từ index 0 -> indexRightNotNumber - 1
            var templateLastEntityCode = "";

            //biến này lưu trữ giá trị số từ indexRightNotNumber -> lastEntityCode.Length - 1
            var numberStringInLastEntityCode = "";

            for (int i = 0; i <= indexRightNotNumber - 1; i++)
            {
                char c = lastEntityCode[i];
                templateLastEntityCode += c;
            }

            for (int i = indexRightNotNumber; i < lastEntityCode.Length; i++)
            {
                char c = lastEntityCode[i];
                numberStringInLastEntityCode += c;
            }

            var sameEntityCodes = await _baseRepository.GetSameCodesAsync(templateLastEntityCode);

            //nếu không parse được sẽ báo lỗi server
            //nhưng các entity code đều được chặn lại và kiểm tra kí tự cuối là số khi insert, update
            var numberInLastEntityCode = Int32.Parse(numberStringInLastEntityCode);

            var newEntityCode = "";
            var loopCount = 1;
            do
            {
                newEntityCode = templateLastEntityCode + (numberInLastEntityCode + loopCount);
                loopCount++;
            } while (sameEntityCodes.Contains(newEntityCode));

            return newEntityCode;
        }

        /// <summary>
        /// Hàm thêm bản ghi
        /// </summary>
        /// <param name="entityCreateDto">Dữ liệu của bản ghi</param>
        /// <returns>Số hàng bị ảnh hưởng</returns>
        /// Created by: TTANH (15/07/2023)
        public async Task<int> InsertAsync(TEntityCreateDto entityCreateDto)
        {
            var entityCreateDtoCode = entityCreateDto.GetType().GetProperty($"{tableName}Code").GetValue(entityCreateDto).ToString();

            var validate = true;

            validate = await _baseValidate.CodeValidate(entityCreateDtoCode);

            var entity = _mapper.Map<TEntity>(entityCreateDto);

            var typeEntity = entity.GetType();

            typeEntity.GetProperty($"{tableName}Id").SetValue(entity, Guid.NewGuid());
            typeEntity.GetProperty("CreatedDate").SetValue(entity, DateTime.Now);
            typeEntity.GetProperty("CreatedBy").SetValue(entity, "TTANH");

            var result = await _baseRepository.InsertAsync(entity);

            return result;
        }

        /// <summary>
        /// Hàm cập nhật thông tin bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <param name="entityUpdateDto">Dữ liệu update bản ghi</param>
        /// <returns>Số hàng bị ảnh hưởng</returns>
        /// Created by: TTANH (15/07/2023)
        public async Task<int> UpdateAsync(Guid id, TEntityUpdateDto entityUpdateDto)
        {
            var entityUpdate = await _baseRepository.GetByIdAsync(id);

            var validate = true;

            if (entityUpdate == null)
            {
                throw new NotFoundException();
            }

            var entityUpdateDtoCode = "";

            var typeEntityUpdateDtoCode = entityUpdateDto.GetType().GetProperty($"{tableName}Code").GetValue(entityUpdateDto);

            if (typeEntityUpdateDtoCode != null)
            {
                entityUpdateDtoCode = typeEntityUpdateDtoCode.ToString();
            }

            var entityUpdateCode = entityUpdate.GetType().GetProperty($"{tableName}Code").GetValue(entityUpdate).ToString();

            if ((entityUpdateDtoCode != entityUpdateCode) && (entityUpdateDtoCode != ""))
            {
                validate = await _baseValidate.CodeValidate(entityUpdateDtoCode);
            }

            if (entityUpdate == null)
            {
                throw new NotFoundException();
            }

            foreach (var property in entityUpdateDto.GetType().GetProperties())
            {
                var propertyName = property?.Name;
                var propertyValue = property?.GetValue(entityUpdateDto);

                if ((propertyName != null) && (propertyValue != null))
                {
                    PropertyInfo propertyInfo = entityUpdate.GetType().GetProperty(propertyName);

                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(entityUpdate, propertyValue, null);
                    }
                }
            }

            var entity = _mapper.Map<TEntity>(entityUpdate);

            var typeEntity = entity.GetType();

            typeEntity.GetProperty("ModifiedDate").SetValue(entity, DateTime.Now);
            typeEntity.GetProperty("ModifiedBy").SetValue(entity, "ANHTT");

            var result = await _baseRepository.UpdateAsync(id, entityUpdate);

            return result;
        }

        /// <summary>
        /// Hàm xóa bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>Số hàng bị ảnh hưởng</returns>
        /// Created by: TTANH (15/07/2023)
        public async Task<int> DeleteAsync(Guid id)
        {
            var entity = await _baseRepository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new NotFoundException();
            }

            var result = await _baseRepository.DeleteAsync(id);

            return result;
        }

        /// <summary>
        /// Hàm xóa nhiều bản ghi
        /// </summary>
        /// <param name="ids">List các id của bản ghi</param>
        /// <returns>Số bản ghi đã xóa</returns>
        /// Created by: TTANH (15/07/2023)
        public async Task<int> DeleteMultipleAsync(List<Guid> ids)
        {
            var result = await _baseRepository.DeleteMultipleAsync(ids);

            return result;
        }
        #endregion
    }
}
