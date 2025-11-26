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
                //Validacion de que tenga cargo
                if(string.IsNullOrEmpty(dto.Cargo)) return BadRequest("El campo Cargo es obligatorio.");
                //Verificar si el email ya existe
                var exists = await _userManager.FindByEmailAsync(dto.Email);
                if (exists is not null) return BadRequest("El correo ya está registrado.");
                

                var identityUser = new IdentityUser
                {
                    Email = dto.Email,
                    UserName = dto.Email
                };

                var createResult = await _userManager.CreateAsync(identityUser, dto.Password);
                if (!createResult.Succeeded)return BadRequest(createResult.Errors.FirstOrDefault()?.Description ?? "No se pudo crear el usuario.");
                //Lista de claims base
                var claims = new List<Claim>
                    {
                    new Claim(ClaimTypes.Name, dto.Email ?? string.Empty),
                    new Claim(ClaimTypes.NameIdentifier, identityUser.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("Cargo", dto.Cargo),
                    new Claim("Nombre", dto.Nombre)
                };
                //Proceso segun el cargo
                if (dto.Cargo == "Empleado") 
                {
                    // Verificar si ya existe un empleado con el mismo nombre
                    var empleadoExistente = await _context.Empleados.FirstOrDefaultAsync(e => e.Nombre == dto.Nombre);
                    if (empleadoExistente is not null) return BadRequest("Ya existe un empleado con ese nombre.");
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

                    //Claims adicionales para empleado
                    claims.Add(new Claim("EmpleadoId", empleado.Id.ToString()));
                    claims.Add(new Claim("TipoEntidad", "Empleado"));

                    if (empleado.Id == 0) { return BadRequest("Error al crear el empleado en la base de datos."); }
                }
                else if (dto.Cargo == "Proveedor") 
                {
                    // Validar campos adicionales para Proveedor
                    if (string.IsNullOrEmpty(dto.Empresa) || string.IsNullOrEmpty(dto.Nit)) return BadRequest("Los campos Empresa y Nit son obligatorios para el cargo Proveedor.");
                    //verificar si ya existe un proveedor con el mismo NIT
                    if (!string.IsNullOrEmpty(dto.Nit))
                    {
                        var proveedorExistente = await _context.Proveedores.FirstOrDefaultAsync(p => p.Nit == dto.Nit);
                        if (proveedorExistente is not null) return BadRequest("Ya existe un proveedor con ese NIT.");
                    }
                    // Crear Proveedor con los datos adicionales
                    var proveedor = new Proveedor
                    {
                        Nit = dto.Nit,
                        NombreVendedor = dto.Nombre,
                        Empresa = dto.Empresa,                        
                        Contacto = dto.Contacto,
                        Email = dto.Email,
                        Direccion = dto.Direccion
                    };
                    _context.Proveedores.Add(proveedor);
                    await _context.SaveChangesAsync();
                    if (proveedor.Id == 0) { return BadRequest("Error al crear el proveedor en la base de datos."); }
                    //Claims adicionales para proveedor
                    claims.Add(new Claim("ProvedorId", proveedor.Id.ToString()));
                    claims.Add(new Claim("Empresa", proveedor.Empresa ?? ""));
                    claims.Add(new Claim("TipoEntidad", "Proveedor"));
                    
                    if(!string.IsNullOrEmpty(proveedor.Nit)) claims.Add(new Claim("Nit", proveedor.Nit));
                }
                else
                {
                    return BadRequest("Cargo no válido. Debe ser 'Empleado' o 'Proveedor'.");
                }
                await _userManager.AddClaimsAsync(identityUser, claims);    

                // Generamos JWT con los mismos datos
                var token = BuildToken(claims);
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

            var claims = (await _userManager.GetClaimsAsync(user)).ToList();

            //agregar claims basicos si faltan
            if(!claims.Any(c => c.Type == ClaimTypes.NameIdentifier))
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            if(!claims.Any(c => c.Type == JwtRegisteredClaimNames.Jti))
                claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            return Ok(BuildToken(claims));
        }

        private TokenDTO BuildToken(List<Claim> claims )
        {
            try 
            {
               
                var jwtKey = _config["Jwt:Key"];
                if (string.IsNullOrEmpty(jwtKey))
                {
                    jwtKey =  "MiEmpresaSultana2025!ClaveSuperSecretaParaJWT@12345";
                    Console.WriteLine(" Warning: JWT key not found in configuration. Using default key.");
                }
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
