using System;
using System.Net.Http;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CurrieTechnologies.Razor.SweetAlert2;
using Sultana.WEB;
using Sultana.WEB.Auth;          // JwtAuthenticationStateProvider, ILoginService, LoginService
using Sultana.WEB.Repositories;
using Sultana.WEB.Services;  // IRepository, Repository

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//servicios de autenticación
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

//Configurar HttpClient
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// ?? Base de la API: pon TU puerto real y DEJA la barra final
var apiBase = builder.Configuration["ApiBaseUrl"] ?? "https://localhost:7000/";
builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(apiBase) });

// Auth 
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

// SweetAlert2 y repositorio
builder.Services.AddSweetAlert2();
builder.Services.AddScoped<IRepository, Repository>();

// Auth provider + login service
builder.Services.AddScoped<JwtAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<JwtAuthenticationStateProvider>()); ;
builder.Services.AddScoped<ILoginService, LoginService>();

await builder.Build().RunAsync();

