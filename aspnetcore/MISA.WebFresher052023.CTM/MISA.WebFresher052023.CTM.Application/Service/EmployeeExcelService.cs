using AutoMapper;
using MISA.WebFresher052023.CTM.Domain;

namespace MISA.WebFresher052023.CTM.Application
{
    public class EmployeeExcelService : ExcelService<Employee, EmployeeDto, EmployeeCreateDto>
    {
        public EmployeeExcelService(IEmployeeRepository employeeRepository, IMapper mapper, IEmployeeValidate employeeValidate, IExcelWorker<EmployeeDto, EmployeeCreateDto> employeeExcelWorker) : base(employeeRepository, mapper, employeeValidate, employeeExcelWorker)
        {
        }
    }
}
