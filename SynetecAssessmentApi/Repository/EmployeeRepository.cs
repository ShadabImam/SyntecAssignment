using Microsoft.EntityFrameworkCore;
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Persistence;
using SynetecAssessmentApi.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {

        private readonly AppDbContext _dbContext;

        public EmployeeRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _dbContext
                .Employees
                .Include(e => e.Department)
                .ToListAsync();
        }

        public async Task<Employee> GetEmployeeWithIdAsync(int id)
        {
            return await _dbContext.Employees
               .Include(e => e.Department)
               .FirstOrDefaultAsync(item => item.Id == id);
        }
    }
}
