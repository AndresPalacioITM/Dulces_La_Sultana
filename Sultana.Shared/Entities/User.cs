using Sultana.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sultana.Shared.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Display(Name = "Correo Electronico")]
        [EmailAddress(ErrorMessage = "El formato del correo no es valido")]
        [Required(ErrorMessage = "El correo es obligatorio")]
        [MaxLength(120)]
        public string Email { get; set; }

        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string PasswordHash { get; set; }

        [Display(Name = "Tipo de Usuario")]
        public TipoUsuario TipoUsuario { get; set; } = TipoUsuario.User;

        [Display(Name = "Activo")]
        public bool Activo { get; set; } = true;

        [Display(Name = "Fecha de Creación")]
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        // Relación opcional con Empleado
        public int? EmpleadoId { get; set; }
        public virtual Empleado Empleado { get; set; }

    }
}
