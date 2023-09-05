using Microsoft.AspNetCore.Mvc;
using RevenueAuthority.Api.Resources.RequestResources;
using RevenueAuthority.Core.Models;
using RevenueAuthority.Core.Services;
using RevenueAuthority.Data.Repositories;

namespace RevenueAuthority.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController: ControllerBase
{
    private bool errorOccurred;
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        this._employeeService = employeeService;
    }

    [HttpGet("GetAllEmployees")]
    public IActionResult GetAllEmployees()
    {
        var employees = _employeeService.GetAllEmployees();
        return Ok(employees);
    }
    
    [HttpGet("GetEmployeeById/{id}")]
    public IActionResult GetEmployeesById(Guid id)
    {
        var employees = _employeeService.GetEmployeesByEmployeeId(id);
        return Ok(employees); 
    }

    [HttpGet("GetAllEmployeesByCompanyId/{companyId}")]
    public IActionResult GetEmployeesByCompanyId(Guid companyId)
    {
        var employees = _employeeService.GetEmployeesByCompanyId(companyId);

        return Ok(employees);
    }
    
    [HttpPost("AddEmployee")]
    [ProducesResponseType(typeof(IDictionary<string, string>), 400)] 
    public IActionResult AddEmployee([FromBody] EmployeeRequestResource request)
    {
        if (String.IsNullOrEmpty(request.Name))
        {
            errorOccurred = true;
            var errors = new Dictionary<string, string>
            {
                { "Name", "The field 'Name' is empty or null" }
            };
            return BadRequest(errors);
        }
        if (request.CompanyId == null)
        {
            errorOccurred = true;
            var errors = new Dictionary<string, string>
            {
                { "CompanyId", "The field 'CompanyId' is empty or null" }
            };
            return BadRequest(errors);
        }
        if (!_employeeService.CompanyExists(request.CompanyId))
        {
            errorOccurred = true;
            var errors = new Dictionary<string, string>
            {
                { "CompanyId", "The CompanyId entered does not exist" }
            };
            return BadRequest(errors);
        }
        if (!ModelState.IsValid)
        {
            errorOccurred = true;
            return BadRequest(ModelState);
        }

        if (errorOccurred) return Ok(request);
        
        _employeeService.AddEmployee(request.Name, request.CompanyId);
        _employeeService.SaveChanges();

        return Ok(request);
    }
    
    [HttpPost("AddEmployeesInBulk")]
    public IActionResult AddEmployees([FromBody] EmployeeBulkRequestResource request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var employeeRequests = new List<EmployeeRequestResource>();
        foreach (var employeeRequest in request.Employees)
        {
            if (String.IsNullOrEmpty(employeeRequest.Name))
            {
                errorOccurred = true;
                var errors = new Dictionary<string, string>
                {
                    { "Name", "The field 'Name' is empty or null in one of the Items" }
                };
                return BadRequest(errors);
            }
            if (employeeRequest.CompanyId == null)
            {
                errorOccurred = true;
                var errors = new Dictionary<string, string>
                {
                    { "CompanyId", $"The CompanyId you entered for {employeeRequest.Name} is empty or null" }
                };
                return BadRequest(errors);
            }
            if (!_employeeService.CompanyExists(employeeRequest.CompanyId))
            {
                errorOccurred = true;
                var errors = new Dictionary<string, string>
                {
                    { "CompanyId", $"The CompanyId you entered for {employeeRequest.Name} doesn't exist" }
                };
                return BadRequest(errors);
            }
            if (errorOccurred) return Ok(request);
            employeeRequests.Add(employeeRequest);
        }
        if (errorOccurred) return Ok(request);
        foreach (var employeeRequest in employeeRequests)
        {
            _employeeService.AddEmployeesBulk(employeeRequest.Name, employeeRequest.CompanyId);
        }
        _employeeService.SaveChanges(); 

        return Ok(request);
    }
    
    
}