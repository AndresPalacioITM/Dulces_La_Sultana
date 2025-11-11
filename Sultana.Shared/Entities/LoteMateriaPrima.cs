using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sultana.Shared.Entities
{
    public class LoteMateriaPrima
    {
        public int IdLoteMP { get; set; }
        public int IdMP { get; set; }
        public int IdProveedor { get; set; }
        public string NumeroLote { get; set; } = null!;
        public DateTime FechaHoraRecepcion { get; set; } = DateTime.UtcNow;
        public DateTime? FechaVencimiento { get; set; }
        public decimal CantidadIngresada { get; set; }
        public decimal CantidadDisponible { get; set; }
        public string InspeccionColor { get; set; } = "CUMPLE";   // 'CUMPLE' | 'NO CUMPLE'
        public string InspeccionOlor { get; set; } = "CUMPLE";
        public string InspeccionTextura { get; set; } = "CUMPLE";
        public string EstadoEmpaque { get; set; } = "CUMPLE";
        public int ResponsableId { get; set; }
        public string? Observaciones { get; set; }
    }
}
