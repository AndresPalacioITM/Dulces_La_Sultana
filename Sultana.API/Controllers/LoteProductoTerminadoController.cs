using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sultana.Shared.Entities;
using Sultana.API.Data; 

namespace Sultana.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoteProductoTerminadoController : ControllerBase
    {
        private readonly DataContext _context;

        public LoteProductoTerminadoController(DataContext context)
        {
            _context = context;
        }

        // Metodo GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoteProductoTerminado>>> GetLotesProductoTerminado()
        {
            var lotes = await _context.LoteProductoTerminados
                .Include(l => l.OrdenProduccion)
                .ToListAsync();

            return lotes;
        }

        // ---------------------------
        // 🔹 Método GET (por id)
        // ---------------------------
        [HttpGet("{id:int}")]
        public async Task<ActionResult<LoteProductoTerminado>> GetLoteProductoTerminado(int id)
        {
            var lote = await _context.LoteProductoTerminados
                .Include(l => l.OrdenProduccion)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (lote == null)
            {
                return NotFound();
            }

            return lote;
        }

        // ---------------------------
        // 🔹 Método POST (crear)
        // ---------------------------
        [HttpPost]
        public async Task<ActionResult<LoteProductoTerminado>> PostLoteProductoTerminado(LoteProductoTerminado lote)
        {
            _context.LoteProductoTerminados.Add(lote);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLoteProductoTerminado), new { id = lote.Id }, lote);
        }

        // ---------------------------
        // 🔹 Método PUT (actualizar)
        // ---------------------------
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutLoteProductoTerminado(int id, LoteProductoTerminado lote)
        {
            if (id != lote.Id)
            {
                return BadRequest();
            }

            _context.Entry(lote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.LoteProductoTerminados.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // Metodo DELETE

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteLoteProductoTerminado(int id)
        {
            var lote = await _context.LoteProductoTerminados.FindAsync(id);
            if (lote == null)
            {
                return NotFound();
            }

            _context.LoteProductoTerminados.Remove(lote);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
