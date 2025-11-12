using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Sultana.WEB;
using Sultana.WEB.Auth;
using Sultana.WEB.Repositories;
using Sultana.WEB;
using Sultana.WEB.Repositories;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Base URL de la API
var apiBase = builder.Configuration["ApiBaseUrl"] ?? "https://localhost:7000";
builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(apiBase) });

// Repositorio genérico (ya lo tienes en Repositories/)
builder.Services.AddScoped<IRepository, Repository>();

// Auth (guardar token en localStorage y exponer AuthenticationState)
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();

await builder.Build().RunAsync();
