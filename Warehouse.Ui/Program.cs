using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using Warehouse.Ui;
using Warehouse.Ui.Services;

const string webApiUrl = "https://localhost:7213/";

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(webApiUrl) });

builder.Services.AddScoped<HttpService>();

builder.Services.AddScoped<ModelEntityService>();

builder.Services.AddRadzenComponents();

await builder.Build().RunAsync();
