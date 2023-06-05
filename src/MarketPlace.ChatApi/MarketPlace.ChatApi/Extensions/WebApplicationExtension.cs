
using MarketPlace.ChatData.Context;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.ChatApi.Extensions;
public static class WebApplicationExtension
{
    public static void MigrateChatDbContext(this WebApplication app)
    {
            if (app.Services.GetService<ChatDbContext>() != null)
            {
                var chatDb = app.Services.GetRequiredService<ChatDbContext>();
                chatDb.Database.Migrate();
            }
    }
}