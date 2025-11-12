// Sultana.API/Controllers/AccountsController.cs
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Sultana.API.Data;
using Sultana.Shared.DTOs;
using Sultana.Shared.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/accounts")]
public class AccountsController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _config;
    private readonly DataContext _context; // tu DbContext con DbSet<Empleado>

    public AccountsController(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        IConfiguration config,
        DataContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _config = config;
        _context = context;
    }

    [HttpPost("register")]
    public async Task<ActionResult<TokenDTO>> Register([FromBody] RegisterDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var exists = await _userManager.FindByEmailAsync(dto.Email);
        if (exists is not null) return BadRequest("El correo ya está registrado.");

        var identityUser = new IdentityUser
        {
            Email = dto.Email,
            UserName = dto.Email
        };

        var createResult = await _userManager.CreateAsync(identityUser, dto.Password);
        if (!createResult.Succeeded)
            return BadRequest(createResult.Errors.FirstOrDefault()?.Description ?? "No se pudo crear el usuario.");

        // Crear Empleado con los MISMOS datos del registro
        var empleado = new Empleado
        {
            Nombre = dto.Nombre,
            Cargo = dto.Cargo,
            Contacto = dto.Contacto,
            Email = dto.Email
        };

        _context.Empleados.Add(empleado);
        await _context.SaveChangesAsync();

        // Opcional: claims con la misma info
        await _userManager.AddClaimsAsync(identityUser, new[]
        {
            new Claim(ClaimTypes.Name, dto.Email),
            new Claim("EmpleadoId", empleado.Id.ToString()),
            new Claim("Nombre", empleado.Nombre),
            new Claim("Cargo", empleado.Cargo ?? string.Empty),
            new Claim("Photo", dto.Photo ?? string.Empty)
        });

        // Generamos JWT con los mismos datos
        var token = BuildToken(identityUser, empleado, dto.Photo);
        return Ok(token);
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenDTO>> Login(LoginDTO dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user is null) return BadRequest("Email o contraseña incorrectos.");

        var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, lockoutOnFailure: false);
        if (!result.Succeeded) return BadRequest("Email o contraseña incorrectos.");

        // recuperar empleado por email para inyectar en claims
        var empleado = _context.Empleados.FirstOrDefault(e => e.Email == dto.Email);
        return Ok(BuildToken(user, empleado));
    }

    private TokenDTO BuildToken(IdentityUser user, Empleado? empleado, string? photo = null)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email ?? string.Empty),
        };

        if (empleado is not null)
        {
            claims.Add(new Claim("EmpleadoId", empleado.Id.ToString()));
            claims.Add(new Claim("Nombre", empleado.Nombre));
            claims.Add(new Claim("Cargo", empleado.Cargo ?? string.Empty));
        }

        if (!string.IsNullOrWhiteSpace(photo))
            claims.Add(new Claim("Photo", photo));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["jwtKey"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var exp = DateTime.UtcNow.AddDays(30);

        var jwt = new JwtSecurityToken(
            claims: claims,
            expires: exp,
            signingCredentials: creds);

        return new TokenDTO
        {
            Token = new JwtSecurityTokenHandler().WriteToken(jwt),
            Expiration = exp
        };
    }
}
