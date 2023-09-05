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
    public class CompanyControllerTests
    {
        [Fact]
        public void GetAllCompanies_ReturnsCompanyList()
        {
            // Arrange
            var mockCompanyService = new Mock<ICompanyService>();
            mockCompanyService.Setup(service => service.GetAllCompanies())
                .Returns(new List<Company> { new Company() });

            var controller = new CompanyController(mockCompanyService.Object);

            // Act
            var result = controller.GetAllCompanies();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var companies = Assert.IsAssignableFrom<IEnumerable<Company>>(okResult.Value);
            Assert.Single(companies);
        }



        [Fact]
        public void GetCompaniesById_ReturnsCompanyListById()
        {
            // Arrange
            var companyId = Guid.NewGuid();
            var mockCompanyService = new Mock<ICompanyService>();
            mockCompanyService.Setup(service => service.GetCompanyByCompanyId(companyId))
                .Returns(new Company());

            var controller = new CompanyController(mockCompanyService.Object);

            // Act
            var result = controller.GetCompaniesById(companyId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var company = Assert.IsType<Company>(okResult.Value);
            Assert.NotNull(company);
        }

        [Fact]
        public void AddCompany_TestInput()
        {
            // Arrange
            var mockCompanyService = new Mock<ICompanyService>();
            var controller = new CompanyController(mockCompanyService.Object);
            var request = new CompanyRequestResource { Name = "Angle Dimension" };

            // Act
            var result = controller.AddCompany(request);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            mockCompanyService.Verify(service => service.AddCompany(It.IsAny<string>()), Times.Once);
        }

    }
}
