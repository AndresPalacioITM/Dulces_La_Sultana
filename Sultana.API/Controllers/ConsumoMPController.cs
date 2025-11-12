using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sultana.API.Data;
using Sultana.Shared.Entities;

namespace Sultana.API.Controllers
{
    [Route("/api/consumomp")]
    [ApiController]
    public class ConsumoMPController : ControllerBase
    {
        private readonly DataContext _context;

        public ConsumoMPController(DataContext context)
        {
            _context = context;
        }

        // Metodo GET
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _context.ConsumoMPs.ToListAsync());
        }

        // GET: /api/consumomp/5
        [HttpGet("{id:long}")]
        public async Task<ActionResult> Get(long id)
        {
            var consumo = await _context.ConsumoMPs
                .FirstOrDefaultAsync(x => x.Id == id);

            if (consumo == null)
            {
                return NotFound();
            }

            return Ok(consumo);
        }

        // POST: /api/consumomp
        [HttpPost]
        public async Task<ActionResult> Post(ConsumoMP consumoMP)
        {
            _context.ConsumoMPs.Add(consumoMP);
            await _context.SaveChangesAsync();
            return Ok(consumoMP); // 200
        }

        // PUT: /api/consumomp
        [HttpPut]
        public async Task<ActionResult> Put(ConsumoMP consumoMP)
        {
            _context.ConsumoMPs.Update(consumoMP);
            await _context.SaveChangesAsync();
            return Ok(consumoMP); // 200
        }

        // DELETE: /api/consumomp/5
        [HttpDelete("{id:long}")]
        public async Task<ActionResult> Delete(long id)
        {
            var filas = await _context.ConsumoMPs
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();

            if (filas == 0)
                return NotFound(); // 404

            return NoContent(); // 204
        }
    }
}
