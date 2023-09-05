namespace RevenueAuthority.Core.Models;

public class Employee
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public Guid? CompanyId { get; set; }
}