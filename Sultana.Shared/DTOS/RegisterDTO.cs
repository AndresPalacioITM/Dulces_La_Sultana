using System.ComponentModel.DataAnnotations;

namespace Sultana.Shared.DTOs;

public class RegisterDTO
{
    [Required, MaxLength(120)]
    public string Nombre { get; set; } = null!;

    [Required, EmailAddress, MaxLength(120)]
    public string Email { get; set; } = null!;

    [Required, MinLength(6)]
    public string Password { get; set; } = null!;

    [Required, MinLength(25)]
    public string Contacto { get; set; } = null!;

    public string? Photo { get; set; }

    public string? Cargo { get; set; } = "Operario";
}

