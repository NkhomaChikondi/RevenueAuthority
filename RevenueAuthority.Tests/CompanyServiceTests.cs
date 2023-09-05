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
    public class CompanyServiceTests
    {
        [Fact]
        public void GetAllCompanies_ReturnsCompanyList()
        {
            // Arrange
            var mockCompanyRepository = new Mock<ICompanyRepository>();
            mockCompanyRepository.Setup(repo => repo.GetAllCompanies())
                .Returns(new List<Company> { new Company() });

            var service = new CompanyService(mockCompanyRepository.Object);

            // Act
            var companies = service.GetAllCompanies();

            // Assert
            Assert.Single(companies);
        }

        [Fact]
        public void GetCompanyByCompanyId_ReturnsCompanyById()
        {
            // Arrange
            var companyId = Guid.NewGuid();
            var mockCompanyRepository = new Mock<ICompanyRepository>();
            mockCompanyRepository.Setup(repo => repo.GetCompanyByCompanyId(companyId))
                .Returns(new Company());

            var service = new CompanyService(mockCompanyRepository.Object);

            // Act
            var company = service.GetCompanyByCompanyId(companyId);

            // Assert
            Assert.NotNull(company);
        }

        [Fact]
        public void AddCompany_ValidData()
        {
            // Arrange
            var mockCompanyRepository = new Mock<ICompanyRepository>();
            var service = new CompanyService(mockCompanyRepository.Object);

            // Act
            service.AddCompany("TestCompany");

            // Assert
            mockCompanyRepository.Verify(repo => repo.AddCompany(It.IsAny<Company>()), Times.Once);
        }
    }
}
