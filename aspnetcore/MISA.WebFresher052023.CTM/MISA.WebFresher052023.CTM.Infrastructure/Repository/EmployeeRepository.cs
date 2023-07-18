using Dapper;
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
    /// Created By: TTANH (12/07/2023)
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        #region Constructor
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {
        }
        #endregion

        #region Methods

        #endregion
    }
}
