using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OasisApi.Dtos.Moradores;
using OasisApi.Services;

namespace OasisApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class MoradoresController : ControllerBase
    {
        private readonly IMoradorService _moradorService;

        public MoradoresController(IMoradorService moradorService)
        {
            _moradorService = moradorService;
        }

        // GET: api/moradores
        [HttpGet]
        public async Task<ActionResult<List<MoradorResponseDto>>> GetMoradores()
        {
            return Ok(await _moradorService.GetAllAsync());
        }

        // GET: api/moradores/MoradorbyId/{id}
        [HttpGet("MoradorbyId/{id}")]
        public async Task<ActionResult<MoradorResponseDto>> GetMoradorbyId(Guid id)
        {
            return Ok(await _moradorService.GetByIdAsync(id));
        }

        // POST: api/moradores
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<MoradorResponseDto>> InserirNovoMorador([FromBody] MoradorCreateDto dto)
        {
            var morador = await _moradorService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetMoradorbyId), new { id = morador.Id }, morador);
        }

        // PUT: api/moradores/{id}
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<MoradorResponseDto>> AtualizarMoradorbyId(Guid id, MoradorUpdateDto dto)
        {
            return Ok(await _moradorService.UpdateAsync(id, dto));
        }

        // DELETE: api/moradores/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteMorador(Guid id)
        {
            await _moradorService.DeleteAsync(id);
            return Ok();
        }
    }
}
