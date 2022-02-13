using SynetecAssessmentApi.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Interfaces
{
    /// <summary>
    /// Service relating to Bonusbool
    /// </summary>
    public interface IBonusPoolService
    {
        /// <summary>
        /// Gets All Emplyoee 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<EmployeeDto>> GetEmployeesAsync();

        /// <summary>
        /// Calulates Bonus Pool
        /// </summary>
        /// <param name="bonusPoolAmount"></param>
        /// <param name="selectedEmployeeId"></param>
        /// <returns></returns>
        Task<BonusPoolCalculatorResultDto> CalculateAsync(int bonusPoolAmount, int selectedEmployeeId);
    }
}
