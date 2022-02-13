using SynetecAssessmentApi.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Interfaces
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Gets all Employee
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<EmployeeDto>> GetEmployeesAsync();

        /// <summary>
        /// Gets Employee With Given Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<EmployeeDto> GetEmployeeWithIdAsync(int id);
    }
}
