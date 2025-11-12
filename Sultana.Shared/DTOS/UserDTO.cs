using Sultana.Shared.Entities;
using System.ComponentModel.DataAnnotations;

namespace Sultana.Shared.DTOs;

public class UserDTO : Empleado
{
    // Opcional para la foto (Empleado no la tiene)
    public string? Photo { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Contraseña")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres.")]
    public string Password { get; set; } = null!;

    [Compare("Password", ErrorMessage = "La contraseña y la confirmación no son iguales.")]
    [Display(Name = "Confirmación de contraseña")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres.")]
    public string PasswordConfirm { get; set; } = null!;
}
