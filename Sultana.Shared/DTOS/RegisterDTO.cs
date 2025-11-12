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

    [MaxLength(20)]
    public string? Document { get; set; }

    [MaxLength(100)]
    public string? FirstName { get; set; }

    [MaxLength(100)]
    public string? LastName { get; set; }

    [MaxLength(200)]
    public string? Direccion { get; set; }

    public string? Photo { get; set; }

    public string? Rol { get; set; } = "Operario";
}

