using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RevenueAuthority.Api.Controllers;
using RevenueAuthority.Api.Resources.RequestResources;
using RevenueAuthority.Core.Models;
using RevenueAuthority.Core.Services;
using Xunit;

namespace YourProject.Tests
{
    public class EmployeeControllerTests
    {
        [Fact]
        public void GetAllEmployees_ReturnsEmployeeList()
        {
            // Arrange
            var mockEmployeeService = new Mock<IEmployeeService>();
            mockEmployeeService.Setup(service => service.GetAllEmployees())
                .Returns(new List<Employee> { new Employee() });

            var controller = new EmployeeController(mockEmployeeService.Object);

            // Act
            var result = controller.GetAllEmployees();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var employees = Assert.IsAssignableFrom<IEnumerable<Employee>>(okResult.Value);
            Assert.Single(employees);
        }

        [Fact]
        public void GetEmployeesById_ReturnsEmployeeListById()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var mockEmployeeService = new Mock<IEmployeeService>();
            mockEmployeeService.Setup(service => service.GetEmployeesByEmployeeId(employeeId))
                .Returns(new Employee());

            var controller = new EmployeeController(mockEmployeeService.Object);

            // Act
            var result = controller.GetEmployeesById(employeeId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var employee = Assert.IsType<Employee>(okResult.Value);
            Assert.NotNull(employee);
        }

        [Fact]
        public void GetEmployeesByCompanyId_ReturnsEmployeeListById()
        {
            // Arrange
            var companyId = Guid.NewGuid();
            var mockEmployeeService = new Mock<IEmployeeService>();
            mockEmployeeService.Setup(service => service.GetEmployeesByCompanyId(companyId))
                .Returns(new List<Employee> { new Employee() });

            var controller = new EmployeeController(mockEmployeeService.Object);

            // Act
            var result = controller.GetEmployeesByCompanyId(companyId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var employees = Assert.IsAssignableFrom<IEnumerable<Employee>>(okResult.Value);
            Assert.Single(employees);
        }
        
        [Fact]
        public void AddEmployees_TestInput()
        {
            // Arrange
            var mockEmployeeService = new Mock<IEmployeeService>();
            mockEmployeeService.Setup(service => service.CompanyExists(It.IsAny<Guid?>()))
                .Returns(true); // Mocking that CompanyExists returns true

            var controller = new EmployeeController(mockEmployeeService.Object);
            var employeeRequest = new EmployeeRequestResource
            {
                Name = "Chikondi Nkhoma",
                CompanyId = Guid.NewGuid() 
            };

            // Act
            var result = controller.AddEmployee(employeeRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(employeeRequest, okResult.Value);
        }

    }
}
