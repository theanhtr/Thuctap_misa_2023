using AutoMapper;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Vml.Office;
using MISA.WebFresher052023.CTM.Domain;

namespace MISA.WebFresher052023.CTM.Application
{
    /// <summary>
    /// Class triển khai employee service được gọi từ controller
    /// </summary>
    /// Created by: TTANH (12/07/2023)
    public class EmployeeService : BaseService<Employee, EmployeeDto, EmployeeCreateDto, EmployeeUpdateDto>, IEmployeeService
    {
        #region Constructor
        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper, IEmployeeValidate employeeValidate) : base(employeeRepository, mapper, employeeValidate)
        {
        }
        #endregion
    }
}
