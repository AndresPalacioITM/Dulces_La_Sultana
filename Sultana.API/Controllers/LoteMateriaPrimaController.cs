using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sultana.API.Data;
using Sultana.Shared.Entities;

namespace Sultana.API.Controllers
{
    [Route("api/lotemateriaprima")]
    [ApiController]
    public class LoteMateriaPrimaController : ControllerBase
    {
        private readonly DataContext _context;

        public LoteMateriaPrimaController(DataContext context)
        {
            _context = context;
        }

        // Metodo GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoteMateriaPrima>>> GetLotesMateriaPrima()
        {
            var lotes = await _context.LoteMateriaPrimas
                .Include(l => l.MateriaPrima)
                .Include(l => l.Proveedor)
                .Include(l => l.Responsable)
                .ToListAsync();

            return Ok(lotes);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<LoteMateriaPrima>> GetLoteMateriaPrima(int id)
        {
            var lote = await _context.LoteMateriaPrimas
                .Include(l => l.MateriaPrima)
                .Include(l => l.Proveedor)
                .Include(l => l.Responsable)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (lote == null)
            {
                return NotFound();
            }

            return Ok(lote);
        }

        // Metodo POST
        [HttpPost]
        public async Task<ActionResult<LoteMateriaPrima>> PostLoteMateriaPrima(LoteMateriaPrima loteMateriaPrima)
        {
            // Validar combinación única de MateriaPrimaId + NumeroLote
            var existe = await _context.LoteMateriaPrimas.AnyAsync(l =>
                l.MateriaPrimaId == loteMateriaPrima.MateriaPrimaId &&
                l.NumeroLote == loteMateriaPrima.NumeroLote);

            if (existe)
            {
                return BadRequest($"Ya existe un lote con el número '{loteMateriaPrima.NumeroLote}' para esta materia prima.");
            }

            _context.LoteMateriaPrimas.Add(loteMateriaPrima);
            await _context.SaveChangesAsync();

            return Ok(loteMateriaPrima);
        }

        // Metodo PUT
        [HttpPut]
        public async Task<ActionResult<LoteMateriaPrima>> PutLoteMateriaPrima(LoteMateriaPrima loteMateriaPrima)
        {
            var existe = await _context.LoteMateriaPrimas.AnyAsync(l => l.Id == loteMateriaPrima.Id);
            if (!existe)
            {
                return NotFound($"No se encontró el lote con Id {loteMateriaPrima.Id}.");
            }

            _context.LoteMateriaPrimas.Update(loteMateriaPrima);
            await _context.SaveChangesAsync();

            return Ok(loteMateriaPrima);
        }

        // Metodo DELETE
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteLoteMateriaPrima(int id)
        {
            var filasAfectadas = await _context.LoteMateriaPrimas
                .Where(l => l.Id == id)
                .ExecuteDeleteAsync();

            if (filasAfectadas == 0)
            {
                return NotFound();
            }

            return NoContent(); // 204
        }
    }
}
