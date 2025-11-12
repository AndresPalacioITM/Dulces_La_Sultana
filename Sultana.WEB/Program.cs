using System;
using System.Net.Http;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CurrieTechnologies.Razor.SweetAlert2;

using Sultana.WEB;
using Sultana.WEB.Auth;          // JwtAuthenticationStateProvider, ILoginService, LoginService
using Sultana.WEB.Repositories;  // IRepository, Repository

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// ?? Base de la API: pon TU puerto real y DEJA la barra final
var apiBase = builder.Configuration["ApiBaseUrl"] ?? "https://localhost:7000/";
builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(apiBase) });

// Auth core para WASM
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

// SweetAlert2
builder.Services.AddSweetAlert2();

// Repositorio HTTP
builder.Services.AddScoped<IRepository, Repository>();

// Auth provider + login service
builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();
builder.Services.AddScoped<ILoginService, LoginService>();

await builder.Build().RunAsync();

