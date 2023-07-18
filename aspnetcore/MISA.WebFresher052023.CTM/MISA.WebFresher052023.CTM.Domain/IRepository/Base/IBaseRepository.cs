namespace MISA.WebFresher052023.CTM.Domain
{
    /// <summary>
    /// Base để xử lý CRUD căn bản và filter
    /// </summary>
    /// Created by: TTANH (18/07/2023)
    public interface IBaseRepository<TEntity> : IReadOnlyCollection<TEntity>
    {
        #region Methods
        /// <summary>
        /// Hàm lọc dữ liệu bản ghi
        /// </summary>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="pageNumber">Thứ tự trang bao nhiêu</param>
        /// <param name="searchText">Chuỗi tìm kiếm</param>
        /// <returns>Các bản lọc theo các tiêu chí</returns>
        /// Created by: TTANH (18/07/2023)
        Task<IEnumerable<TEntity>> FilterAsync(int? pageSize, int? pageNumber, string? searchText);

        /// <summary>
        /// Hàm thêm bản ghi
        /// </summary>
        /// <param name="entity">Dữ liệu của bản ghi</param>
        /// <returns>Số hàng bị ảnh hưởng</returns>
        /// Created by: TTANH (18/07/2023)
        Task<int> InsertAsync(TEntity entity);

        /// <summary>
        /// Hàm cập nhật thông tin bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <param name="entity">Dữ liệu update bản ghi</param>
        /// <returns>Số hàng bị ảnh hưởng</returns>
        /// Created by: TTANH (18/07/2023)
        Task<int> UpdateAsync(Guid id, TEntity entity);

        /// <summary>
        /// Hàm xóa bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>Số hàng bị ảnh hưởng</returns>
        /// Created by: TTANH (18/07/2023)
        Task<int> DeleteAsync(Guid id);

        /// <summary>
        /// Hàm xóa nhiều bản ghi
        /// </summary>
        /// <param name="ids">List các id của bản ghi</param>
        /// <returns>Số bản ghi đã xóa</returns>
        /// Created by: TTANH (18/07/2023)
        Task<int> DeleteMultipleAsync(List<Guid> ids);
        #endregion
    }
}
