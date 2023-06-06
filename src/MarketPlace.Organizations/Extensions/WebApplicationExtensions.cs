using MarketPlace.Organizations.Context;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.Organizations.Extensions;

public static class WebApplicationExtensions
{
	public static void MigrateChatDbContext(this WebApplication app)
	{
		if (app.Services.GetService<OrganizationsDbContext>() != null)
		{
			var chatDb = app.Services.GetRequiredService<OrganizationsDbContext>();
			chatDb.Database.Migrate();
		}
	}
}