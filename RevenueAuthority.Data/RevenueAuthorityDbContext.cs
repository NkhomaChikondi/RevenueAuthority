using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RevenueAuthority.Core.Models;
namespace RevenueAuthority.Data;

/// <summary>
/// This stores the information of the Companies and Employees in Json files
/// </summary>
public class RevenueAuthorityDbContext
{
    private readonly string _companiesFilePath = "JSON Files/companies.json";
    private readonly string _employeesFilePath = "JSON Files/employees.json";

    public List<Company> Companies { get; set; }
    public List<Employee> Employees { get; set; }

    public RevenueAuthorityDbContext()
    {
        LoadData();
    }
    /// <summary>
    /// Loads the Json file 
    /// </summary>
    private void LoadData()
    {
        string dataFolderPath = Path.GetDirectoryName(_companiesFilePath);
        //Create new folder if it doesnt exist
        if (!Directory.Exists(dataFolderPath))
        {
            Directory.CreateDirectory(dataFolderPath);
        }
        //Creates new Json files if they dont exist
        if (!File.Exists(_companiesFilePath))
        {
            File.WriteAllText(_companiesFilePath, "[]");
        }
    
        if (!File.Exists(_employeesFilePath))
        {
            File.WriteAllText(_employeesFilePath, "[]");
        }
        
        //Adds contents of the Json files to the lists
        Companies = JsonConvert.DeserializeObject<List<Company>>(File.ReadAllText(_companiesFilePath));
        Employees = JsonConvert.DeserializeObject<List<Employee>>(File.ReadAllText(_employeesFilePath));
    }

    /// <summary>
    /// Writes the contents of the lists to the Json files
    /// </summary>
    public void SaveChanges()
    {
        File.WriteAllText(_companiesFilePath, JsonConvert.SerializeObject(Companies));
        File.WriteAllText(_employeesFilePath, JsonConvert.SerializeObject(Employees));
    }
    
}