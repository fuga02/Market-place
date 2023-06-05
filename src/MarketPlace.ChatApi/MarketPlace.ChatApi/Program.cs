using MarketPlace.ChatApi.Hubs;
using MarketPlace.ChatApi.Services;
using MarketPlace.ChatData.Context;
using MarketPlace.ChatData.Managers;
using MarketPlace.IdentityData.Context;
using MarketPlace.IdentityData.Extensions;
using MarketPlace.IdentityData.Middlewares;
using MarketPlace.ChatApi.Extensions;
using MarketPlace.ChatApi.Hubs;
using MarketPlace.ChatApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Description = "JWT Bearer. : \"Authorization: Bearer { token } \"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddDbContext<ChatDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ChatDb"));
});

builder.Services.AddScoped<ChatManager>();
builder.Services.AddScoped<UserConnectionIdService>();
builder.Services.AddSignalR();
var app = builder.Build();

app.UseCors(cors =>
{
    cors.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
});

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.MigrateChatDbContext();
app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();


app.UseErrorHandlerMiddleware();
app.MapHub<ConversationHub>("/hubs/conversation");

app.MapControllers();

app.Run();
