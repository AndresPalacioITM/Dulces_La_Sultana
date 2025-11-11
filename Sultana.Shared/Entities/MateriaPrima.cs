using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sultana.Shared.Entities
{
    public class MateriaPrima
    {
        public int IdMP { get; set; }
        public string Nombre { get; set; } = null!;
        public string UnidadMedida { get; set; } = "kg";   // 'kg','g','L','ml'
        public decimal StockMinimo { get; set; } = 0;
    }
}
