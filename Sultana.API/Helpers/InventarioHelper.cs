using Microsoft.EntityFrameworkCore;
using Sultana.API.Data;
using Sultana.Shared.Entities;

namespace Sultana.API.Helpers
{
    public class InventarioHelper : IInventarioHelper
    {
        private readonly DataContext _context;
        public InventarioHelper(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> DescontarMateriaPrimaAsync(int loteId, decimal cantidad)
        {
            var lote = await _context.LoteMateriaPrimas.FirstOrDefaultAsync(l => l.Id == loteId);
            if (lote == null)
            {
                return false; // Lote no encontrado o cantidad insuficiente
            }
            if (lote.CantidadDisponible < cantidad)
            {
                throw new InvalidOperationException("No hay suficiente materia prima disponible en este lote.");
            }
            lote.CantidadDisponible -= cantidad;
            await _context.SaveChangesAsync();
            return true;
        }    

    }
}
