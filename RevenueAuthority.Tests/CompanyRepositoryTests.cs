using System;
using System.Collections.Generic;
using Moq;
using RevenueAuthority.Core.Models;
using RevenueAuthority.Data;
using RevenueAuthority.Data.Repositories;
using Xunit;

namespace YourProject.Tests
{
    public class CompanyRepositoryTests
    {
        [Fact]
        public void GetAllCompanies_ReturnsCompanyList()
        {
            // Arrange
            var dbContext = new RevenueAuthorityDbContext();
            var repository = new CompanyRepository(dbContext);

            // Act
            var companies = repository.GetAllCompanies();

            // Assert
            Assert.NotNull(companies);
            Assert.IsType<List<Company>>(companies);
        }

        [Fact]
        public void GetCompanyByCompanyId_ReturnsCompanyListById()
        {
            // Arrange
            var dbContext = new RevenueAuthorityDbContext();
            var repository = new CompanyRepository(dbContext);
            var companyId = Guid.NewGuid();

            // Act
            var company = repository.GetCompanyByCompanyId(companyId);

            // Assert
            Assert.Null(company); 
        }
    }
}