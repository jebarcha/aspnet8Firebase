using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var baseUrl = builder.Configuration.GetValue<string>("BaseUrl");
builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(baseUrl!) });

await builder.Build().RunAsync();
