using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Dtos;
using SynetecAssessmentApi.Interfaces;
using SynetecAssessmentApi.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
        {
            IEnumerable<Employee> employees = await _employeeRepository.GetAllEmployeesAsync();

            List<EmployeeDto> result = new List<EmployeeDto>();

            foreach (var employee in employees)
            {
                result.Add(
                    new EmployeeDto
                    {
                        Fullname = employee.Fullname,
                        JobTitle = employee.JobTitle,
                        Salary = employee.Salary,
                        Department = new DepartmentDto
                        {
                            Title = employee.Department.Title,
                            Description = employee.Department.Description
                        }
                    });
            }

            return result;
        }

        public async Task<EmployeeDto> GetEmployeeWithIdAsync(int id)
        {
            Employee employee = await _employeeRepository.GetEmployeeWithIdAsync(id);

            return new EmployeeDto
            {
                Fullname = employee.Fullname,
                JobTitle = employee.JobTitle,
                Salary = employee.Salary,
                Department = new DepartmentDto
                {
                    Title = employee.Department.Title,
                    Description = employee.Department.Description
                }
            };
        }
    }
}
