using System;
using System.Collections.Generic;
using RevenueAuthority.Core.Models;

namespace RevenueAuthority.Core.Repositories
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployeesByEmployeeId(Guid id);
        List<Employee> GetEmployeesByCompanyId(Guid companyId);
        void AddEmployeesBulk(List<Employee> employees);
        void AddEmployee(Employee employee);
        bool CompanyExists(Guid? companyId);
        void SaveChanges();
    }
}