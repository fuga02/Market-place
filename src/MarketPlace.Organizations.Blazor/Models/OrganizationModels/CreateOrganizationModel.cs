using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace MarketPlace.Organizations.Blazor.Models.OrganizationModels;

public class CreateOrganizationModel
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public IFormFile Logo { get; set; }
    public string? Contact { get; set; }
    public List<CreateAddressModel> Addresses { get; set; } = new List<CreateAddressModel>();
}