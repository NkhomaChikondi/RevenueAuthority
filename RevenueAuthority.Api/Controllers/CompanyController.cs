using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RevenueAuthority.Api.Resources.RequestResources;
using RevenueAuthority.Core.Models;
using RevenueAuthority.Core.Services;
using RevenueAuthority.Data.Repositories;

namespace RevenueAuthority.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompanyController: ControllerBase
{
    private readonly ICompanyService _companyService;

    public CompanyController(ICompanyService companyService)
    {
        this._companyService = companyService;
    }

    [HttpGet("GetAllCompanies")]
    public IActionResult GetAllCompanies()
    {
        var companies = _companyService.GetAllCompanies();
        return Ok(companies);
    }
    
    [HttpGet("GetCompanyById/{id}")]
    public IActionResult GetCompaniesById(Guid id)
    {
        var company = _companyService.GetCompanyByCompanyId(id);

        return Ok(company); 
    }

    [HttpPost("AddCompany")]
    [ProducesResponseType(typeof(IDictionary<string, string>), 400)] 
    public IActionResult AddCompany([FromBody] CompanyRequestResource request)
    {
        if (String.IsNullOrEmpty(request.Name))
        {
            var errors = new Dictionary<string, string>
            {
                { "Name", "The field 'Name' is empty or null" }
            };
            return BadRequest(errors);
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        _companyService.AddCompany(request.Name);
        _companyService.SaveChanges(); 

        return Ok(request);
    }


}