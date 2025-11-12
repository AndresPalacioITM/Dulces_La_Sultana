using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sultana.Shared.Entities;
using Sultana.API.Data; 

namespace Sultana.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MateriaPrimaController : ControllerBase
    {
        private readonly DataContext _context;

        public MateriaPrimaController(DataContext context)
        {
            _context = context;
        }

        // Metodo GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MateriaPrima>>> GetMateriasPrimas()
        {
            return await _context.MateriaPrimas.ToListAsync();
        }

      
        [HttpGet("{id:int}")]
        public async Task<ActionResult<MateriaPrima>> GetMateriaPrima(int id)
        {
            var materia = await _context.MateriaPrimas.FindAsync(id);

            if (materia == null)
            {
                return NotFound();
            }

            return materia;
        }

        // Metodo POST
        [HttpPost]
        public async Task<ActionResult<MateriaPrima>> PostMateriaPrima(MateriaPrima materia)
        {
            _context.MateriaPrimas.Add(materia);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMateriaPrima), new { id = materia.Id }, materia);
        }

        // Metodo PUT
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutMateriaPrima(int id, MateriaPrima materia)
        {
            if (id != materia.Id)
            {
                return BadRequest();
            }

            _context.Entry(materia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.MateriaPrimas.Any(e => e.Id == id))
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
        public async Task<IActionResult> DeleteMateriaPrima(int id)
        {
            var materia = await _context.MateriaPrimas.FindAsync(id);
            if (materia == null)
            {
                return NotFound();
            }

            _context.MateriaPrimas.Remove(materia);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
