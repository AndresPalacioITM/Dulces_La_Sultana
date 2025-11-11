using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sultana.Shared.Entities
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Nit { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Empresa { get; set; }
        public string? Direccion { get; set; }
        public string? Contacto { get; set; }
    }

}
