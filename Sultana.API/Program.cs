using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Sultana.API.Data;
using Sultana.API.Helpers;
using Sultana.API.Services;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging(logging =>
{
    logging.AddConsole();
    logging.AddDebug();
    logging.AddFilter("Microsoft", LogLevel.Warning);
    logging.AddFilter("System", LogLevel.Warning);
    logging.AddFilter("Sultana.API", LogLevel.Debug);
});

// ============= CORS =============
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        policy
            .WithOrigins(
                "https://sultanaweb-f9dkeae2cqe0hghq.brazilsouth-01.azurewebsites.net",
                "https://localhost:8000",
                "https://localhost:7000",
                "https://localhost:7001",
                "https://localhost:5000",
                "https://localhost:5001"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
        // IMPORTANTE: con tokens en header NO necesitas AllowCredentials
        // Si algún día usas cookies de auth, ahí sí lo revisamos.
    });
});

// Add services to the container.
builder.Services.AddControllers();

// Configuración Entity Framework y SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(connectionString,sqlServerOptions => sqlServerOptions.EnableRetryOnFailure(maxRetryCount:5,maxRetryDelay:TimeSpan.FromSeconds(30),errorNumbersToAdd:null)));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;

    options.User.RequireUniqueEmail = true;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
})
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

// Swagger / OpenAPI
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. <br /> <br />
                      Enter 'Bearer' [space] and then your token in the text input below.<br /> <br />
                      Example: 'Bearer 12345abcdef'<br /> <br />",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

// Helpers y servicios
builder.Services.AddScoped<IAlertaHelper, AlertaHelper>();
builder.Services.AddHostedService<AlertaBackgroundService>();
builder.Services.AddScoped<IInventarioHelper, InventarioHelper>();

builder.Services.AddOpenApi();

var app = builder.Build();

// Crear admin por defecto
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
        await CreateAdminUser(userManager);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Error");
    }
}

// OpenAPI solo en desarrollo
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// ===== Pipeline =====
app.UseHttpsRedirection();

// IMPORTANTE: CORS debe ir ANTES de Authentication/Authorization
app.UseRouting();
app.UseCors("AllowBlazorClient");

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
});

// Mapear controladores
app.MapControllers();


app.MapControllers();

app.Run();

// ===== Método helper para crear admin =====
async Task CreateAdminUser(UserManager<IdentityUser> userManager)
{
    var adminEmail = "admin@sultana.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new IdentityUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };
        var result = await userManager.CreateAsync(adminUser, "Admin123@");
        if (result.Succeeded)
        {
            await userManager.AddClaimAsync(adminUser, new Claim("Cargo", "admin"));
            await userManager.AddClaimAsync(adminUser, new Claim("Nombre", "Administrador"));
        }
    }
}