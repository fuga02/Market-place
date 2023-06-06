using MarketPlace.Organizations.Entities;

namespace MarketPlace.Organizations.Models;

public class OrganizationUserModel
{
    public Guid UserId { get; set; }
    public OrganizationUserRole UserRole { get; set; }
}