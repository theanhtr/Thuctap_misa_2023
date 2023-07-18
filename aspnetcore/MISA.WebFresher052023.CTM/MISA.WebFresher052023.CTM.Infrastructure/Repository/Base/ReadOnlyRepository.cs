namespace MISA.WebFresher052023.CTM.Infrastructure
{
    /// <summary>
    /// Base xử lý việc đọc với id
    /// </summary>
    /// CreatedBy: TTANH (18/07/2023)
    public class ReadOnlyRepository<TEntity> : IReadOnlyCollection<TEntity>
    {
        #region Methods
        /// <summary>
        /// Hàm đếm số bản ghi trong db
        /// </summary>
        /// <returns>Số bản ghi trong db</returns>
        /// Created by: TTANH (18/07/2023)
        public async Task<int> CountAsync()
        {

        }

        /// <summary>
        /// Hàm lấy tất cả bản ghi
        /// </summary>
        /// <returns>Mảng chứa các bản ghi</returns>
        /// Created by: TTANH (18/07/2023)
        public async Task<IEnumerable<TEntity>?> GetAsync()
        {

        }

        /// <summary>
        /// Hàm lấy một bản ghi theo id
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>Bản ghi, throw exception nếu không tìm thấy</returns>
        /// Created by: TTANH (18/07/2023)
        public async Task<TEntity> GetByIdAsync(Guid id)
        {

        }

        /// <summary>
        /// Hàm tìm kiếm bản ghi, trả về null nếu không tìm thấy
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>Bản ghi, null nếu không tìm thấy</returns>
        /// Created by: TTANH (18/07/2023)
        public async Task<TEntity?> FindAsync(Guid id)
        {

        }

        /// <summary>
        /// Hàm lấy ra bản ghi được thêm gần nhất
        /// </summary>
        /// <returns>Bản ghi được thêm gần nhất</returns>
        /// CreatedBy: TTANH (18/07/2023)
        public async Task<TEntity> GetLastInsertAsync()
        {

        }
        #endregion
    }
}
