using Dapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.Extensions.Configuration;
using MISA.WebFresher052023.CTM.Domain;
using MySqlConnector;
using System.Data;
using System.Data.Common;

namespace MISA.WebFresher052023.CTM.Infrastructure
{
    /// <summary>
    /// Triển khai bằng dapper và mysql
    /// </summary>
    /// Created By: TTANH (15/07/2023)
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {
        #region Fields
        protected readonly DbConnection _dbConnection;
        private readonly string tableName;
        #endregion

        #region Constructor
        public BaseRepository(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            _dbConnection = new MySqlConnection(connectionString);
            tableName = typeof(TEntity).Name;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hàm đếm số bản ghi trong db
        /// </summary>
        /// <returns>Số bản ghi trong db</returns>
        /// Created by: TTANH (15/07/2023)
        public async Task<int> CountAsync()
        {
            var countEntities = await _dbConnection.ExecuteScalarAsync<int>($"SELECT COUNT(*) FROM {tableName};", null);

            return countEntities;
        }

        /// <summary>
        /// Hàm lấy tất cả bản ghi
        /// </summary>
        /// <returns>Mảng chứa các bản ghi</returns>
        /// Created by: TTANH (15/07/2023)
        public async Task<IEnumerable<TEntity>?> GetAsync()
        {
            var entities = await _dbConnection.QueryAsync<TEntity>($"SELECT * FROM {tableName};", null);

            return entities;
        }

        /// <summary>
        /// Hàm lấy một bản ghi theo id
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>Bản ghi</returns>
        /// Created by: TTANH (15/07/2023)
        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            var param = new DynamicParameters();
            param.Add("id", id);

            var entity = await _dbConnection.QueryFirstOrDefaultAsync<TEntity>($"SELECT * FROM {tableName} entity WHERE entity.{tableName}Id = @id;", param);

            return entity;
        }

        /// <summary>
        /// Hàm lấy một bản ghi theo mã code
        /// </summary>
        /// <param name="code">Mã code của bản ghi</param>
        /// <returns>Bản ghi</returns>
        /// Created by: TTANH (15/07/2023)
        public async Task<TEntity> GetByCodeAsync(string code)
        {
            var param = new DynamicParameters();
            param.Add("code", code);

            var entity = await _dbConnection.QueryFirstOrDefaultAsync<TEntity>($"SELECT * FROM {tableName} entity WHERE entity.{tableName}Code = @code;", param);

            return entity;
        }

        /// <summary>
        /// Hàm lấy ra bản ghi được thêm gần nhất
        /// </summary>
        /// <returns>Bản ghi được thêm gần nhất</returns>
        /// CreatedBy: TTANH (15/07/2023)
        public async Task<TEntity> GetLastInsertAsync()
        {
            var entity = await _dbConnection.QueryFirstOrDefaultAsync<TEntity>($"SELECT * FROM {tableName} entity ORDER BY entity.CreatedDate DESC LIMIT 1;", null);

            return entity;
        }

        /// <summary>
        /// Hàm lọc dữ liệu bản ghi
        /// </summary>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="pageNumber">Thứ tự trang bao nhiêu</param>
        /// <param name="searchText">Chuỗi tìm kiếm</param>
        /// <returns>Các bản lọc theo các tiêu chí</returns>
        /// Created by: TTANH (15/07/2023)
        public async Task<IEnumerable<TEntity>> FilterAsync(int? pageSize, int? pageNumber, string? searchText)
        {
            var param = new DynamicParameters();
            param.Add("pageSize", pageSize);
            param.Add("pageNumber", pageNumber);
            param.Add("searchText", searchText);

            var entities = await _dbConnection.QueryAsync<TEntity>($"Proc_{tableName}_Filter", param, commandType: CommandType.StoredProcedure);

            return entities;
        }

        /// <summary>
        /// Hàm lấy ra tất cả các code có dạng giống với code đầu vào
        /// </summary>
        /// <param name="templateCode">Mã code đầu vào</param>
        /// <returns>Chuỗi các code</returns>
        /// CreatedBy: TTANH (15/07/2023)
        public async Task<IEnumerable<string>> GetSameCodesAsync(string templateCode)
        {
            var param = new DynamicParameters();
            param.Add("code", templateCode);

            var codes = await _dbConnection.QueryAsync<string>($"SELECT entity.{tableName}Code FROM {tableName} entity WHERE entity.{tableName}Code LIKE CONCAT(@code, '%');", param);

            return codes;
        }

        /// <summary>
        /// Hàm thêm bản ghi
        /// </summary>
        /// <param name="entity">Dữ liệu của bản ghi</param>
        /// <returns>Số hàng bị ảnh hưởng</returns>
        /// Created by: TTANH (15/07/2023)
        public async Task<int> InsertAsync(TEntity entity)
        {
            var param = new DynamicParameters();

            var type = typeof(TEntity);
            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                var propertyName = "@" + property.Name;
                var propertyValue = property.GetValue(entity);

                param.Add(propertyName, propertyValue);
            }

            var result = await _dbConnection.ExecuteAsync($"Proc_{tableName}_Insert", param, commandType: CommandType.StoredProcedure);

            return result;
        }

        /// <summary>
        /// Hàm cập nhật thông tin bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <param name="entity">Dữ liệu update bản ghi</param>
        /// <returns>Số hàng bị ảnh hưởng</returns>
        /// Created by: TTANH (15/07/2023)
        public async Task<int> UpdateAsync(Guid id, TEntity entity)
        {
            var param = new DynamicParameters();
            param.Add("id", id);

            var type = typeof(TEntity);
            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                var propertyName = "@" + property.Name;
                var propertyValue = property.GetValue(entity);

                param.Add(propertyName, propertyValue);
            }

            var result = await _dbConnection.ExecuteAsync($"Proc_{tableName}_Update", param, commandType: CommandType.StoredProcedure);

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
            var param = new DynamicParameters();
            param.Add("id", id);

            var result = await _dbConnection.ExecuteAsync($"DELETE FROM {tableName} WHERE {tableName}Id = @id;", param);

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
            var param = new DynamicParameters();
            param.Add("ids", ids);

            var result = await _dbConnection.ExecuteAsync($"DELETE FROM {tableName} WHERE {tableName}Id IN @ids;", param);

            return result;
        }
        #endregion
    }
}
