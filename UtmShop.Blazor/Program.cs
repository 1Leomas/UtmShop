using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using UtmShop.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var defaultUri = new Uri(builder.HostEnvironment.BaseAddress);
var utmShopApiUri = new Uri("https://utmshop.azurewebsites.net/");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = utmShopApiUri
});

await builder.Build().RunAsync();
