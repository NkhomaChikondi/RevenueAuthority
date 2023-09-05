using Microsoft.EntityFrameworkCore;
using RevenueAuthority.Core.Models;
using RevenueAuthority.Core.Repositories;

namespace RevenueAuthority.Data.Repositories;
    /// <summary>
    /// Responsible for adding taking a company list and adding it the Dbcontext(Json File)
    /// </summary>
    public class CompanyRepository: ICompanyRepository
    {
        private readonly RevenueAuthorityDbContext _dbContext; 

        public CompanyRepository(RevenueAuthorityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Company> GetAllCompanies()
        {
            return _dbContext.Companies.ToList(); 
        }
    
        public Company GetCompanyByCompanyId(Guid id)
        {
            var companies = _dbContext.Companies;
            var company = Array.Find(companies.ToArray(), cmpny => cmpny.Id == id);
            return company;
        }

        public void AddCompany(Company company)
        {
            _dbContext.Companies.Add(company); 
        }
        
        public void SaveChanges()
        {
            _dbContext.SaveChanges(); // Save changes to JSON files
        }
    }