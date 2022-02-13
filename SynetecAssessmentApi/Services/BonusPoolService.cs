using SynetecAssessmentApi.Dtos;
using SynetecAssessmentApi.Infrastructure;
using SynetecAssessmentApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Services
{
    public class BonusPoolService : IBonusPoolService
    {
        private readonly IEmployeeService _employeeService;

        public BonusPoolService(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
        {
            return await _employeeService.GetEmployeesAsync();
        }

        public async Task<BonusPoolCalculatorResultDto> CalculateAsync(int bonusPoolAmount, int selectedEmployeeId)
        {
            if (selectedEmployeeId <= 0)
            {
                throw new SyntechException(new ArgumentException("valid Id Needed", nameof(selectedEmployeeId)), SyntechExceptionType.InvalidData);
            }

            //load the details of the selected employee using the Id
            var employee = await _employeeService.GetEmployeeWithIdAsync(selectedEmployeeId);

            if (employee == null)
            {
                throw new SyntechException(new ArgumentNullException(), SyntechExceptionType.InvalidData);
            }

            //load all employee
            var employees = await _employeeService.GetEmployeesAsync();
            //get the total salary budget for the company
            int totalSalary = (int)employees.Sum(item => item.Salary);

            //calculate the bonus allocation for the employee
            decimal bonusPercentage = (decimal)employee.Salary / (decimal)totalSalary;
            int bonusAllocation = (int)(bonusPercentage * bonusPoolAmount);

            return new BonusPoolCalculatorResultDto
            {
                Employee = employee,
                Amount = bonusAllocation
            };
        }
    }
}
