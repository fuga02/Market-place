using MarketPlace.Organizations.Managers;
using MarketPlace.Organizations.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Organizations.Controllers;

[Route("api/organization")]
[ApiController]/*
[Authorize]*/
public class OrganizationsController : ControllerBase
{
    private readonly OrganizationManager _organizationManager;

    public OrganizationsController(OrganizationManager organizationManager)
    {
        _organizationManager = organizationManager;
    }


    [HttpGet]
    public async Task<IActionResult> GetOrganizations()
    {

        return Ok(await _organizationManager.GetOrganizations());
    }
    [HttpPost("create")]
    public async Task<IActionResult> CreateOrganization([FromForm] CreateOrganizationModel organizationModel)
    {
        return Ok(await _organizationManager.Create(organizationModel));
    }

    [HttpGet("{organizationId}")]
    public async Task<IActionResult> GetById(Guid organizationId)
    {
        return Ok(await _organizationManager.GetById(organizationId));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateOrganization(Guid organizationId, [FromForm] CreateOrganizationModel organizationModel)
    {
        return Ok(await _organizationManager.Update(organizationId, organizationModel));
    }

    
}