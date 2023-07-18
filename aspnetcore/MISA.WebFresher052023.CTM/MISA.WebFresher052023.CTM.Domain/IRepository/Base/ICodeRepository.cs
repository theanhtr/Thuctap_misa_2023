namespace MISA.WebFresher052023.CTM.Domain
{
    /// <summary>
    /// Base để xử lý đối tượng có thêm mã code
    /// </summary>
    /// CreatedBy: TTANH (18/07/2023)
    public interface ICodeRepository<TEntity> : IBaseRepository<TEntity>
    {
        #region Methods
        /// <summary>
        /// Hàm lấy một bản ghi theo mã code
        /// </summary>
        /// <param name="code">Mã code của bản ghi</param>
        /// <returns>Bản ghi</returns>
        /// Created by: TTANH (18/07/2023)
        Task<TEntity> GetByCodeAsync(string code);

        /// <summary>
        /// Hàm lấy ra tất cả các code có dạng giống với code đầu vào
        /// </summary>
        /// <param name="templateCode">Mã code đầu vào</param>
        /// <returns>Chuỗi các code</returns>s
        /// CreatedBy: TTANH (18/07/2023)
        Task<IEnumerable<string>> GetSameCodesAsync(string templateCode);
        #endregion
    }
}
