using Sultana.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sultana.Shared.DTOs;

public class UserDTO : Empleado
{
   
    [Required, MaxLength(20)]
    public string Document { get; set; } = null!;

    [Required, MaxLength(120)]
    public string FirstName { get; set; } = null!;
    [Required, MaxLength(120)]
    public string LastName { get; set; } = null!;

    [EmailAddress, Required, MaxLength(120)]
    public string Email { get; set; } = null!;

    [MaxLength(50)]
    public string? Cargo { get; set; }

    [MaxLength(200)]
    public string? Direccion { get; set; }

    public string? Photo { get; set; }

   
    [DataType(DataType.Password)]
    [Display(Name = "Contraseña")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "El campo {0} debe tener entre {2} y {1} carácteres.")]
    public string Password { get; set; } = null!;

    [Compare("Password", ErrorMessage = "La contraseña y la confirmación no son iguales.")]
    [Display(Name = "Confirmación de contraseña")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "El campo {0} debe tener entre {2} y {1} carácteres.")]
    public string PasswordConfirm { get; set; } = null!;
}
