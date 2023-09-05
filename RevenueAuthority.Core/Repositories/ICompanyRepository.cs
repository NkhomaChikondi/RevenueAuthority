using RevenueAuthority.Core.Models;

namespace RevenueAuthority.Core.Repositories;

public interface ICompanyRepository
{
    IEnumerable<Company> GetAllCompanies();
    Company GetCompanyByCompanyId(Guid id);
    
    void AddCompany(Company company);
    void SaveChanges();
}