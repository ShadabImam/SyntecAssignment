using Moq;
using NUnit.Framework;
using SynetecAssessmentApi.Dtos;
using SynetecAssessmentApi.Infrastructure;
using SynetecAssessmentApi.Interfaces;
using SynetecAssessmentApi.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Test
{
    public class BonusPoolServiceTest
    {
        private IBonusPoolService _sut;
        private Mock<IEmployeeService> _employeeServiceMock;

        [SetUp]
        public void Setup()
        {
            _employeeServiceMock = new Mock<IEmployeeService>();
            _sut = new BonusPoolService(_employeeServiceMock.Object);
        }

        [Test]
        public async Task CalculateAsyncs_SyntechException_InvalidId_Test()
        {
            Assert.ThrowsAsync<SyntechException>(async () => await _sut.CalculateAsync(1, 1));
        }

        [Test]
        public async Task CalculateAsync_SyntechException_EmployeeNotFound_Test()
        {
            _employeeServiceMock.Setup(es => es.GetEmployeeWithIdAsync(1)).Returns(Task.FromResult<EmployeeDto>(null));
            Assert.ThrowsAsync<SyntechException>(async () => await _sut.CalculateAsync(1, 1));
        }

        [Test]
        public async Task CalculateAsync_DivideByZeroException_TotalEmpCountZero_Test()
        {
            var empDto = new EmployeeDto
            {
                Fullname = "TestName",
                JobTitle = "TestTitle",
                Salary = 100,
                Department = new DepartmentDto
                {
                    Title = "TestDepartmentTitle",
                    Description = "TestDepartentDescription"
                }
            };
            _employeeServiceMock.Setup(es => es.GetEmployeeWithIdAsync(1)).Returns(Task.FromResult<EmployeeDto>(empDto));
            Assert.ThrowsAsync<DivideByZeroException>(async () => await _sut.CalculateAsync(1, 1));
        }

        [Test]
        public async Task CalculateAsync_ReturnValidResult_TestAsync()
        {
            var empDto = new EmployeeDto
            {
                Fullname = "TestName",
                JobTitle = "TestTitle",
                Salary = 100,
                Department = new DepartmentDto
                {
                    Title = "TestDepartmentTitle",
                    Description = "TestDepartentDescription"
                }
            };

            _employeeServiceMock.Setup(es => es.GetEmployeesAsync()).Returns(Task.FromResult<IEnumerable<EmployeeDto>>(new List<EmployeeDto> { empDto }));

            _employeeServiceMock.Setup(es => es.GetEmployeeWithIdAsync(1)).Returns(Task.FromResult<EmployeeDto>(empDto));

            var result = await _sut.CalculateAsync(100, 1);

            Assert.AreEqual(result.Employee.Fullname, empDto.Fullname);
            Assert.AreEqual(result.Employee.JobTitle, empDto.JobTitle);
            Assert.AreEqual(result.Employee.Salary, empDto.Salary);

            Assert.AreEqual(result.Employee.Department.Title, empDto.Department.Title);
            Assert.AreEqual(result.Employee.Department.Description, empDto.Department.Description);
            Assert.AreEqual(result.Amount, 100);
        }
    }
}