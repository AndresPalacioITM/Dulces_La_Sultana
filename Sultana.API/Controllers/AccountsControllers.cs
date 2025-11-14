// Sultana.API/Controllers/AccountsController.cs
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Sultana.API.Data;
using Sultana.Shared.DTOs;
using Sultana.Shared.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sultana.API.Controllers
{

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
            try 
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var exists = await _userManager.FindByEmailAsync(dto.Email);
                if (exists is not null) return BadRequest("El correo ya está registrado.");
                var empleadoExistente = await _context.Empleados.FirstOrDefaultAsync(e => e.Nombre == dto.Nombre);
                if (empleadoExistente is not null) return BadRequest("Ya existe un empleado con ese nombre.");

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

                if (empleado.Id == 0) { return BadRequest("Error al crear el empleado en la base de datos."); }

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
            catch (Exception ex) {
                Console.WriteLine($"Error en Register: {ex.Message}");
                return StatusCode(500, "Error interno del servidor.");
            } 
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
            return Ok(BuildToken(user, empleado,null));
        }

        private TokenDTO BuildToken(IdentityUser user, Empleado? empleado, string? photo = null)
        {
            try 
            {
                // Puedes agregar más claims si es necesario
                var jwtKey = _config["Jwt:Key"];
                if (string.IsNullOrEmpty(jwtKey))
                {
                    jwtKey =  "MiEmpresaSultana2025!ClaveSuperSecretaParaJWT@12345";
                    Console.WriteLine(" Warning: JWT key not found in configuration. Using default key.");
                }
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email ?? string.Empty),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

                if (empleado is not null)
                {
                    claims.Add(new Claim("EmpleadoId", empleado.Id.ToString()));
                    claims.Add(new Claim("Nombre", empleado.Nombre));
                    claims.Add(new Claim("Cargo", empleado.Cargo ?? string.Empty));
                }
                else 
                {
                    Console.WriteLine("Empleado es null al generar Token.");
                }

                if (!string.IsNullOrWhiteSpace(photo))
                    claims.Add(new Claim("Photo", photo));

                

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var exp = DateTime.UtcNow.AddDays(30);

                var jwt = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"] ?? "https://localhost:7000",
                    audience: _config["Jwt:Audience"] ?? "https://localhost:8000",
                    claims: claims,
                    expires: exp,
                    signingCredentials: creds);

                return new TokenDTO
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(jwt),
                    Expiration = exp
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al generar el token: {ex.Message}");
                throw;
            }
            
        }
    }
}
