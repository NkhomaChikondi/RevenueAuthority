using RevenueAuthority.Core.Models;

namespace RevenueAuthority.Core.Services;

public interface ICompanyService
{
    IEnumerable<Company> GetAllCompanies();
    Company GetCompanyByCompanyId(Guid id);
    void AddCompany(string name);
    void SaveChanges();
}