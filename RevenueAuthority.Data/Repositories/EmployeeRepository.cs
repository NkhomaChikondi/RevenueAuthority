using RevenueAuthority.Core.Models;
using RevenueAuthority.Core.Repositories;

namespace RevenueAuthority.Data.Repositories;

/// <summary>
/// Responsible for adding taking an employee list and adding it the Dbcontext(Json File)
/// </summary>
public class EmployeeRepository: IEmployeeRepository
{
    private readonly RevenueAuthorityDbContext _dbContext; 

    public EmployeeRepository(RevenueAuthorityDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Employee> GetAllEmployees()
    {
        return _dbContext.Employees.ToList(); 
    }
    
    public Employee GetEmployeesByEmployeeId(Guid id)
    {
        var employees = _dbContext.Employees;
        var employee = Array.Find(employees.ToArray(), emp => emp.Id == id);
        return employee;
    }
    
    public List<Employee> GetEmployeesByCompanyId(Guid companyId)
    {
        var employeesByCompany = _dbContext.Employees.Where(e => e.CompanyId == companyId).ToList();

        return employeesByCompany;
    }

    public void AddEmployee(Employee employee)
    {
        var newEmployee = new Employee
        {
            Id = employee.Id,
            Name = employee.Name,
            CompanyId = employee.CompanyId
        };
        foreach (var c in _dbContext.Companies)
        {
            if (c.Id == employee.CompanyId)
            {
               
                if (c.Employees == null || c.Employees.Count == 0)
                {
                    c.Employees = new List<Employee> { newEmployee };
                }
                else
                {
                     c.Employees.Add(newEmployee);
                }
            }
        }
        _dbContext.Employees.Add(newEmployee); 
    }

    /// <summary>
    /// Checks if a company exists.
    /// </summary>
    /// <returns></returns>
    public bool CompanyExists(Guid? companyId)
    {
        var value = false;
        var hasCompanyId = _dbContext.Companies.Any(company => company.Id == companyId);
        if (hasCompanyId)
        {
            value = true;
        }
        return value;
    }

    public void AddEmployeesBulk(List<Employee> employees)
    {
        foreach (var employee in employees)
        {
            var newEmployee = new Employee
            {
                Id = employee.Id,
                Name = employee.Name,
                CompanyId = employee.CompanyId
            };
            foreach (var c in _dbContext.Companies)
            {
                if (c.Id == employee.CompanyId)
                {
               
                    if (c.Employees == null || c.Employees.Count == 0)
                    {
                        c.Employees = new List<Employee> { newEmployee };
                    }
                    else
                    {
                        c.Employees.Add(newEmployee);
                    }
                }
            }
        }
        _dbContext.Employees.AddRange(employees); 
    }
    public void SaveChanges()
    {
        _dbContext.SaveChanges(); // Save changes to JSON files
    }
}