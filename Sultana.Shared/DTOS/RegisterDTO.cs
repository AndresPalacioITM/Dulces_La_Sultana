// Sultana.Shared/DTOs/RegisterDTO.cs
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Sultana.Shared.DTOs;

public class RegisterDTO
{
    [Display(Name = "Nombre del empleado")]
    [Required, MaxLength(120)]
    public string Nombre { get; set; } = null!;

    [Display(Name = "Cargo")]
    [Required, MaxLength(80)]
    public string Cargo { get; set; } = null!;

    [Display(Name = "Contacto")]
    [MaxLength(60)]
    public string? Contacto { get; set; }

    [Display(Name = "Correo")]
    [Required, EmailAddress, MaxLength(120)]
    public string Email { get; set; } = null!;

    // Opcional (avatar)
    [JsonIgnore]
    public string? Photo { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Contraseña")]
    [Required, StringLength(20, MinimumLength = 6)]
    public string Password { get; set; } = null!;

    [DataType(DataType.Password)]
    [Display(Name = "Confirmación de contraseña")]
    [Required, StringLength(20, MinimumLength = 6)]
    [Compare("Password", ErrorMessage = "La contraseña y la confirmación no son iguales.")]
    public string PasswordConfirm { get; set; } = null!;

    //Componentes adicional para proveedores 

    public string? Nit { get; set; } 
    
    public string? Empresa { get; set; }
    
    public string? Direccion { get; set; }

    }
    

