using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ClientAppdotnet;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiBaseAddress = new Uri("http://localhost:5024");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = apiBaseAddress });

await builder.Build().RunAsync();
