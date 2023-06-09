using Blazored.LocalStorage;
using MarketPlace.Organizations.Blazor;
using MarketPlace.Organizations.Blazor.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddBlazoredLocalStorageAsSingleton();
builder.Services.AddScoped<OrganizationService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:7075") });

await builder.Build().RunAsync();
