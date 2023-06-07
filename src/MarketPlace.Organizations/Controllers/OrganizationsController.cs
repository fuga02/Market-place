using MarketPlace.Organizations.Managers;
using MarketPlace.Organizations.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Organizations.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OrganizationsController : ControllerBase
{
    private readonly OrganizationManager _organizationManager;

    public OrganizationsController(OrganizationManager organizationManager)
    {
        _organizationManager = organizationManager;
    }


    [HttpGet("getList")]
    public async Task<IActionResult> GetOrganizations()
    {

        return Ok(await _organizationManager.GetOrganizations());
    }
    [HttpPost("create")]
    public async Task<IActionResult> CreateOrganization(CreateOrganizationModel organizationModel)
    {
        return Ok(await _organizationManager.Create(organizationModel));
    }

    [HttpGet("getById")]
    public async Task<IActionResult> GetById(Guid organizationId)
    {
        return Ok(await _organizationManager.GetById(organizationId));
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateOrganization(Guid organizationId,CreateOrganizationModel organizationModel)
    {
        return Ok(await _organizationManager.Update(organizationId, organizationModel));
    }

    
}