 using MarketPlace.Organizations.Context;
 using MarketPlace.Organizations.Entities;
 using MarketPlace.Organizations.Providers;
 using Microsoft.AspNetCore.Mvc;
 using Microsoft.AspNetCore.Mvc.Filters;
 using Microsoft.EntityFrameworkCore;

 namespace MarketPlace.Organizations.Filters
{
    public class OrganizationOwnerFilterAttribute:ActionFilterAttribute
    {
        private readonly UserProvider _userProvider;
        private readonly OrganizationsDbContext _context;
        public OrganizationOwnerFilterAttribute(UserProvider userProvider, OrganizationsDbContext context)
        {
            _userProvider = userProvider;
            _context = context;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var organizationId = context.ActionArguments["organizationId"];
            var organization = await _context.Organizations.Include(o => o.Users)
                .Where(o => o.Id == Guid.Parse((string)organizationId!))
                .FirstOrDefaultAsync();

            var userId = _userProvider.UserId;
            var isOwner = organization.Users.Any(u => u.UserId == userId && u.UserRole == OrganizationUserRole.Owner);
            if (!isOwner)
            {
                context.Result = new ForbidResult();
            }
        }
    }

    public class OrganizationOwner : TypeFilterAttribute
    {
        public OrganizationOwner() : base(typeof(OrganizationOwnerFilterAttribute))
        {
        }
    }
}
