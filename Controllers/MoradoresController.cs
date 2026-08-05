using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OasisApi.Application.Dtos.Common;
using OasisApi.Application.Dtos.Moradores;
using OasisApi.Application.Services;
using OasisApi.Domain.Enums;

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

        // GET: api/moradores?nome=&ativo=&page=&pageSize=
        [HttpGet]
        public async Task<ActionResult<PagedResultDto<MoradorResponseDto>>> GetMoradores([FromQuery] MoradorQueryDto query)
        {
            return Ok(await _moradorService.GetAllAsync(query));
        }

        // GET: api/moradores/MoradorbyId/{id}
        [HttpGet("MoradorbyId/{id}")]
        public async Task<ActionResult<MoradorResponseDto>> GetMoradorbyId(Guid id)
        {
            return Ok(await _moradorService.GetByIdAsync(id));
        }

        // POST: api/moradores
        [HttpPost]
        [Authorize(Roles = nameof(TipoUsuario.AssistenteSocial))]
        public async Task<ActionResult<MoradorResponseDto>> InserirNovoMorador([FromBody] MoradorCreateDto dto)
        {
            var morador = await _moradorService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetMoradorbyId), new { id = morador.Id }, morador);
        }

        // PUT: api/moradores/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = nameof(TipoUsuario.AssistenteSocial))]
        public async Task<ActionResult<MoradorResponseDto>> AtualizarMoradorbyId(Guid id, MoradorUpdateDto dto)
        {
            return Ok(await _moradorService.UpdateAsync(id, dto));
        }

        // DELETE: api/moradores/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(TipoUsuario.AssistenteSocial))]
        public async Task<IActionResult> DeleteMorador(Guid id)
        {
            await _moradorService.DeleteAsync(id);
            return Ok();
        }
    }
}
