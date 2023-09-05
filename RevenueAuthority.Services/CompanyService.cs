using RevenueAuthority.Core;
using RevenueAuthority.Core.Models;
using RevenueAuthority.Core.Repositories;
using RevenueAuthority.Core.Services;
using RevenueAuthority.Data.Repositories;

namespace RevenueAuthority.Services;

public class CompanyService: ICompanyService
{
    private readonly ICompanyRepository _companyRepository;

    public CompanyService(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public IEnumerable<Company> GetAllCompanies()
    {
        return _companyRepository.GetAllCompanies();
    }
    
    public Company GetCompanyByCompanyId(Guid id)
    {
        return _companyRepository.GetCompanyByCompanyId(id);
    }
    
    public void AddCompany(string name)
    {
        var newCompany = new Company()
        {
            Id = Guid.NewGuid(),
            Name = name,
        };

        _companyRepository.AddCompany(newCompany);
        _companyRepository.SaveChanges();
    }


    public void SaveChanges()
    {
        _companyRepository.SaveChanges();
    }

}