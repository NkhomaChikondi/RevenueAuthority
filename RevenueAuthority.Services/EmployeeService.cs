using RevenueAuthority.Core.Models;
using RevenueAuthority.Core.Repositories;
using RevenueAuthority.Core.Services;
using RevenueAuthority.Data.Repositories;

namespace RevenueAuthority.Services;

public class EmployeeService: IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public IEnumerable<Employee> GetAllEmployees()
    {
        return _employeeRepository.GetAllEmployees();
    }

    public Employee GetEmployeesByEmployeeId(Guid id)
    {
        return _employeeRepository.GetEmployeesByEmployeeId(id);
    }

    public List<Employee> GetEmployeesByCompanyId(Guid companyId)
    {
        return _employeeRepository.GetEmployeesByCompanyId(companyId);
    }

    public void AddEmployeesBulk(string employeeName, Guid? companyId)
    {
        var newEmployees = new List<Employee>();
        // Create a new employee with the provided data
        var newEmployee = new Employee
        {
            Id = Guid.NewGuid(),
            Name = employeeName,
            CompanyId = companyId 
        };
        newEmployees.Add(newEmployee);
            _employeeRepository.AddEmployeesBulk(newEmployees);
    }
    

    public void AddEmployee(string employeeName, Guid? companyId)
    {
        var newEmployee = new Employee
        {
            Id = Guid.NewGuid(),
            Name = employeeName,
            CompanyId = companyId 
        };
        _employeeRepository.AddEmployee(newEmployee);
        _employeeRepository.SaveChanges();
    }
    
    
    public bool CompanyExists(Guid? companyId)
    {
        return _employeeRepository.CompanyExists(companyId);
    }

    public void SaveChanges()
    {
        _employeeRepository.SaveChanges();
    }

}