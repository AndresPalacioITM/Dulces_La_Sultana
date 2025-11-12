using Microsoft.AspNetCore.Mvc;
using Sultana.Shared.Entities;

namespace Sultana.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsumoMPController : ControllerBase
    {
        private readonly IConsumoMPService _consumoMPService;
        public ConsumoMPController(IConsumoMPService consumoMPService)
        {
            _consumoMPService = consumoMPService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var consumos = await _consumoMPService.GetAllAsync();
            return Ok(consumos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConsumoMP>> GetbyId(long id)
        {
            var consumo = await _consumoMPService.GetByIdAsync(id);
            if (consumo == null)
            {
                return NotFound();
            }
            return Ok(consumo);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ConsumoMP consumoMP)
        {
            var createdConsumo = await _consumoMPService.CreateAsync(consumoMP);
            return CreatedAtAction(nameof(GetbyId), new { id = createdConsumo.Id }, createdConsumo);
        }

        [HttpPut("{id}"]
        public async Task<ActionResult> Update(long id, [FromBody] ConsumoMP consumoMP)
        {
            var updatedConsumo = await _consumoMPService.UpdateAsync(id, consumoMP);
            
            if (!updatedConsumo)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var deleted = await _consumoMPService.DeleteAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }




    }
