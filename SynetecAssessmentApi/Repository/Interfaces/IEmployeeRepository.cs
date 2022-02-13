using SynetecAssessmentApi.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Repository.Interfaces
{
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Gets All Employee
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();

        /// <summary>
        /// Gets Employee with Given Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Employee> GetEmployeeWithIdAsync(int id);
    }
}
