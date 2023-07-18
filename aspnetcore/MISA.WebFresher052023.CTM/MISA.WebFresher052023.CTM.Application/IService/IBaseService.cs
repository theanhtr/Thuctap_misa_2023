namespace MISA.WebFresher052023.CTM.Application
{
    /// <summary>
    /// Base Interface để kế thừa
    /// </summary>
    /// Created by: TTANH (15/07/2023)
    public interface IBaseService<TEntity, TEntityDto, TEntityCreateDto, TEntityUpdateDto>
    {
        #region Methods
        /// <summary>
        /// Hàm lấy tất cả bản ghi
        /// </summary>
        /// <returns>Mảng chứa các bản ghi</returns>
        /// Created by: TTANH (15/07/2023)
        Task<IEnumerable<TEntityDto>?> GetAsync();

        /// <summary>
        /// Hàm lấy một bản ghi theo id
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>Bản ghi</returns>
        /// Created by: TTANH (15/07/2023)
        Task<TEntityDto> GetByIdAsync(Guid id);

        /// <summary>
        /// Hàm lấy một bản ghi theo mã code
        /// </summary>
        /// <param name="code">Mã code của bản ghi</param>
        /// <returns>Bản ghi</returns>
        /// Created by: TTANH (15/07/2023)
        Task<TEntityDto> GetByCodeAsync(string code);

        /// <summary>
        /// Hàm lọc dữ liệu bản ghi
        /// </summary>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="pageNumber">Thứ tự bao nhiêu</param>
        /// <param name="searchText">Chuỗi tìm kiếm</param>
        /// <returns>Các bản ghi lọc theo các tiêu chí trên</returns>
        /// Created by: TTANH (15/07/2023)
        Task<BaseFilterResponse<TEntityDto>> FilterAsync(int? pageSize, int? pageNumber, string? searchText);

        /// <summary>
        /// Hàm lấy một mã code mới không trùng
        /// </summary>
        /// <returns>Mã code mới</returns>
        /// Created by: TTANH (15/07/2023)
        Task<string> GetNewCodeAsync();

        /// <summary>
        /// Hàm thêm bản ghi
        /// </summary>
        /// <param name="entityCreateDto">Dữ liệu của bản ghi</param>
        /// <returns>Số hàng bị ảnh hưởng</returns>
        /// Created by: TTANH (15/07/2023)
        Task<int> InsertAsync(TEntityCreateDto entityCreateDto);

        /// <summary>
        /// Hàm cập nhật thông tin bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <param name="entityUpdateDto">Dữ liệu update bản ghi</param>
        /// <returns>Số hàng bị ảnh hưởng</returns>
        /// Created by: TTANH (15/07/2023)
        Task<int> UpdateAsync(Guid id, TEntityUpdateDto entityUpdateDto);

        /// <summary>
        /// Hàm xóa bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>Số hàng bị ảnh hưởng</returns>
        /// Created by: TTANH (15/07/2023)
        Task<int> DeleteAsync(Guid id);

        /// <summary>
        /// Hàm xóa nhiều bản ghi
        /// </summary>
        /// <param name="ids">List các id của bản ghi</param>
        /// <returns>Số bản ghi đã xóa</returns>
        /// Created by: TTANH (15/07/2023)
        Task<int> DeleteMultipleAsync(List<Guid> ids);
        #endregion
    }
}
