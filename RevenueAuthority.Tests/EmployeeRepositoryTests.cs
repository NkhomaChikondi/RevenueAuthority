using System;
using System.Collections.Generic;
using Moq;
using RevenueAuthority.Core.Models;
using RevenueAuthority.Data;
using RevenueAuthority.Data.Repositories;
using Xunit;

namespace YourProject.Tests
{
    public class EmployeeRepositoryTests
    {
        [Fact]
        public void GetAllEmployees_ReturnsEmployeeList()
        {
            // Arrange
            var dbContext = new RevenueAuthorityDbContext();
            var repository = new EmployeeRepository(dbContext);

            // Act
            var employees = repository.GetAllEmployees();

            // Assert
            Assert.NotNull(employees);
            Assert.IsType<List<Employee>>(employees);
        }

        [Fact]
        public void GetEmployeesByEmployeeId_ReturnsEmployeeList()
        {
            // Arrange
            var dbContext = new RevenueAuthorityDbContext();
            var repository = new EmployeeRepository(dbContext);
            var employeeId = Guid.NewGuid();

            // Act
            var employee = repository.GetEmployeesByEmployeeId(employeeId);

            // Assert
            Assert.Null(employee);
        }
    }
}