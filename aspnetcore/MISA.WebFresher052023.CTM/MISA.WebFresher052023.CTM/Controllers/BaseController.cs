using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher052023.CTM.Application;

namespace MISA.WebFresher052023.CTM.Controllers
{
    /// <summary>
    /// Controller base gồm CRUD, NewCode, Filter
    /// </summary>
    /// <typeparam name="TEntity">Đối tượng chính</typeparam>
    /// <typeparam name="TEntityDto">Đối tượng trả về</typeparam>
    /// <typeparam name="TEntityCreateDto">Đối tượng tạo</typeparam>
    /// <typeparam name="TEntityUpdateDto">Đối tượng cập nhật</typeparam>
    /// Created by: TTANH (15/07/2023)
    [ApiController]
    public class BaseController<TEntity, TEntityDto, TEntityCreateDto, TEntityUpdateDto> : ControllerBase
    {
        #region Fields
        private readonly IBaseService<TEntity, TEntityDto, TEntityCreateDto, TEntityUpdateDto> _baseService;
        #endregion

        #region Constructor
        public BaseController(IBaseService<TEntity, TEntityDto, TEntityCreateDto, TEntityUpdateDto> baseService)
        {
            _baseService = baseService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hàm lấy dữ liệu bản ghi
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>
        /// CreatedBy: TTANH (15/07/2023)
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var entities = await _baseService.GetAsync();

            return StatusCode(StatusCodes.Status200OK, entities);
        }

        /// <summary>
        /// Hàm lấy dữ liệu bản ghi theo id
        /// </summary>
        /// <param name="id">id của bản ghi</param>
        /// <returns>Bản ghi</returns>
        /// CreatedBy: TTANH (15/07/2023)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var entity = await _baseService.GetByIdAsync(id);

            return StatusCode(StatusCodes.Status200OK, entity);
        }

        /// <summary>
        /// Hàm lấy dữ liệu bản ghi theo mã code
        /// </summary>
        /// <param name="code">mã code bản ghi</param>
        /// <returns>Bản ghi</returns>
        /// CreatedBy: TTANH (15/07/2023)
        [HttpGet("code/{code}")]
        public async Task<IActionResult> GetByCodeAsync(string code)
        {
            var entity = await _baseService.GetByCodeAsync(code);

            return StatusCode(StatusCodes.Status200OK, entity);
        }

        /// <summary>
        /// Hàm lọc dữ liệu bản ghi
        /// </summary>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="pageNumber">Thứ tự bao nhiêu</param>
        /// <param name="searchText">Chuỗi lọc</param>
        /// <returns>Các bản ghi lọc theo các tiêu chí trên</returns>
        /// Created by: TTANH (15/07/2023) 
        [HttpGet("filter")]
        public async Task<IActionResult> FiltersAsync([FromQuery] int? pageSize, [FromQuery] int? pageNumber, [FromQuery] string? searchText)
        {
            var filterData = await _baseService.FilterAsync(pageSize, pageNumber, searchText);

            return StatusCode(StatusCodes.Status200OK, filterData);
        }

        /// <summary>
        /// Hàm lấy một entity code mới không trùng
        /// </summary>
        /// <returns> code mới</returns>
        /// Created by: TTANH (15/07/2023)
        [HttpGet("new-code")]
        public async Task<IActionResult> GetNewCodeAsync()
        {
            var newCode = await _baseService.GetNewCodeAsync();

            return StatusCode(StatusCodes.Status200OK, newCode);
        }

        /// <summary>
        /// Hàm thêm bản ghi
        /// </summary>
        /// <param name="entityCreateDto">dữ liệu bản ghi được thêm</param>
        /// <returns>Số hàng bị ảnh hưởng</returns>
        /// CreatedBy: TTANH (15/07/2023)
        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] TEntityCreateDto entityCreateDto)
        {
            var result = await _baseService.InsertAsync(entityCreateDto);

            return StatusCode(StatusCodes.Status201Created, result);
        }

        /// <summary>
        /// Hàm update thông tin bản ghi
        /// </summary>
        /// <param name="id">id của bản ghi</param>
        /// <param name="entityUpdateDto">dữ liệu update</param>
        /// <returns>Số hàng bị ảnh hưởng</returns>
        /// CreatedBy: TTANH (15/07/2023)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] TEntityUpdateDto entityUpdateDto)
        {
            var result = await _baseService.UpdateAsync(id, entityUpdateDto);

            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Hàm xóa bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>Số hàng bị ảnh hưởng</returns>
        /// Created by: TTANH (15/07/2023)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _baseService.DeleteAsync(id);

            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Hàm xóa nhiều bản ghi
        /// </summary>
        /// <param name="ids">List các id của bản ghi</param>
        /// <returns>Số bản ghi đã xóa</returns>
        /// Created by: TTANH (17/07/2023)
        [HttpDelete]
        public async Task<IActionResult> DeleteMultipleAsync([FromBody] List<Guid> ids)
        {
            var result = await _baseService.DeleteMultipleAsync(ids);

            return StatusCode(StatusCodes.Status200OK, result);
        }
        #endregion
    }
}
