using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using static Dapper.SqlMapper;
using System.Data;
using System.Data.Common;
using MISA.WebFresher052023.CTM.Domain;

namespace MISA.WebFresher052023.CTM.Infrastructure
{
    /// <summary>
    /// Triển khai bằng dapper và mysql
    /// </summary>
    /// Created By: TTANH (12/07/2023)
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        #region Fields

        #endregion

        #region Constructor
        public DepartmentRepository(IConfiguration configuration) : base(configuration)
        {
        }
        #endregion

        #region Methods

        #endregion

    }
}
