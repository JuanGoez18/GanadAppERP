using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ERP.Blazor;
using ERP.Blazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5223/") });

// 🔹 Registra tus servicios personalizados
builder.Services.AddScoped<SesionService>();
builder.Services.AddScoped<AdminUsuarioService>();
builder.Services.AddScoped<RolService>();


await builder.Build().RunAsync();
