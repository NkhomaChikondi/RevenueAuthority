using RevenueAuthority.Core.Models;

namespace RevenueAuthority.Core.Services;

public interface IEmployeeService
{
    IEnumerable<Employee> GetAllEmployees();
    Employee GetEmployeesByEmployeeId(Guid id);
    List<Employee> GetEmployeesByCompanyId(Guid companyId);
    void AddEmployeesBulk(string employeeName, Guid? companyId);
    void AddEmployee(string employeeName, Guid? companyId);
    bool CompanyExists(Guid? companyId);
    void SaveChanges();
}