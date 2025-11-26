using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Sultana.WEB;
using Sultana.WEB.AutenticationProviders;
using Sultana.WEB.Auth;          // JwtAuthenticationStateProvider, ILoginService, LoginService
using Sultana.WEB.Repositories;
using Sultana.WEB.Services;  // IRepository, Repository
using System;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//servicios de autenticación
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

//Configurar HttpClient
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// ?? Base de la API: pon TU puerto real y DEJA la barra final
var apiBase = builder.Configuration["ApiBaseUrl"] ?? "https://sultanaapi.azurewebsites.net/";
builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(apiBase) });

// Auth 
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

// SweetAlert2 y repositorio
builder.Services.AddSweetAlert2();
builder.Services.AddScoped<IRepository, Repository>();

// Auth provider + login service
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationProviderJWT>();
builder.Services.AddScoped<ILoginService, AuthenticationProviderJWT>();
await builder.Build().RunAsync();

