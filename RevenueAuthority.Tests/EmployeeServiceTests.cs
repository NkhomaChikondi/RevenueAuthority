using System;
using System.Collections.Generic;
using Moq;
using RevenueAuthority.Core.Models;
using RevenueAuthority.Core.Repositories;
using RevenueAuthority.Core.Services;
using RevenueAuthority.Services;
using Xunit;

namespace YourProject.Tests
{
    public class EmployeeServiceTests
    {
        [Fact]
        public void GetAllEmployees_ReturnsEmployeeList()
        {
            // Arrange
            var mockEmployeeRepository = new Mock<IEmployeeRepository>();
            mockEmployeeRepository.Setup(repo => repo.GetAllEmployees())
                .Returns(new List<Employee> { new Employee() });

            var service = new EmployeeService(mockEmployeeRepository.Object);

            // Act
            var employees = service.GetAllEmployees();

            // Assert
            Assert.Single(employees);
        }

        [Fact]
        public void GetEmployeesByEmployeeId_ReturnsEmployeeById()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var mockEmployeeRepository = new Mock<IEmployeeRepository>();
            mockEmployeeRepository.Setup(repo => repo.GetEmployeesByEmployeeId(employeeId))
                .Returns(new Employee());

            var service = new EmployeeService(mockEmployeeRepository.Object);

            // Act
            var employee = service.GetEmployeesByEmployeeId(employeeId);

            // Assert
            Assert.NotNull(employee);
        }

        [Fact]
        public void GetEmployeesByCompanyId_ReturnsEmployeeListById()
        {
            // Arrange
            var companyId = Guid.NewGuid();
            var mockEmployeeRepository = new Mock<IEmployeeRepository>();
            mockEmployeeRepository.Setup(repo => repo.GetEmployeesByCompanyId(companyId))
                .Returns(new List<Employee> { new Employee() });

            var service = new EmployeeService(mockEmployeeRepository.Object);

            // Act
            var employees = service.GetEmployeesByCompanyId(companyId);

            // Assert
            Assert.Single(employees);
        }

        [Fact]
        public void AddEmployee_ValidData()
        {
            // Arrange
            var mockEmployeeRepository = new Mock<IEmployeeRepository>();
            var service = new EmployeeService(mockEmployeeRepository.Object);

            // Act
            service.AddEmployee("John Doe", Guid.NewGuid());

            // Assert
            mockEmployeeRepository.Verify(repo => repo.AddEmployee(It.IsAny<Employee>()), Times.Once);
        }
    }
}
