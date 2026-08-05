using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OasisApi.Dtos.Alojamentos;
using OasisApi.Dtos.Moradores;
using OasisApi.Services;

namespace OasisApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AlojamentosController : ControllerBase
    {
        private readonly IAlojamentoService _alojamentoService;

        public AlojamentosController(IAlojamentoService alojamentoService)
        {
            _alojamentoService = alojamentoService;
        }

        // GET: api/alojamentos
        [HttpGet]
        public async Task<ActionResult<List<AlojamentoListItemDto>>> GetAlojamentos()
        {
            return Ok(await _alojamentoService.GetAllAsync());
        }

        // GET: api/alojamentos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AlojamentoResponseDto>> GetAlojamentoById(Guid id)
        {
            return Ok(await _alojamentoService.GetByIdAsync(id));
        }

        // POST: api/Alojamentos
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<AlojamentoResponseDto>> InserirNovoAlojamento([FromBody] AlojamentoCreateDto dto)
        {
            var alojamento = await _alojamentoService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetAlojamentoById), new { id = alojamento.Id }, alojamento);
        }

        // PUT: api/alojamentos/Atualizar
        [HttpPut("Atualizar")]
        [Authorize]
        public async Task<ActionResult<AlojamentoResponseDto>> AtualizarAlojamento(Guid id, AlojamentoUpdateDto dto)
        {
            return Ok(await _alojamentoService.UpdateAsync(id, dto));
        }

        // DELETE: api/alojamentos/DeletarAlojamentobyId
        [HttpDelete("DeletarAlojamentobyId")]
        [Authorize]
        public async Task<IActionResult> DeleteAlojamento(Guid id)
        {
            await _alojamentoService.DeleteAsync(id);
            return Ok();
        }

        [HttpPost("{alojamentoId}/adicionar-morador")]
        [Authorize]
        public async Task<ActionResult<MoradorResponseDto>> AdicionarMoradorAoAlojamento(Guid alojamentoId, [FromBody] MoradorCreateDto dto)
        {
            return Ok(await _alojamentoService.AdicionarMoradorAsync(alojamentoId, dto));
        }

        // Listar moradores de um alojamento
        [HttpGet("{alojamentoId}/filtro")]
        public async Task<ActionResult<List<MoradorResponseDto>>> ListarMoradoresDoAlojamento(Guid alojamentoId)
        {
            return Ok(await _alojamentoService.ListarMoradoresAsync(alojamentoId));
        }

        [HttpPost("{alojamentoId}/mudarmorador")]
        [Authorize]
        public async Task<IActionResult> MudarAlojamentoOuFilaDeEspera([FromBody] MudancaAlojamentoRequestDto dto)
        {
            var message = await _alojamentoService.MudarAlojamentoOuFilaDeEsperaAsync(dto);
            return Ok(new { Message = message });
        }

        [HttpGet("{alojamentoId}/fila-de-espera")]
        public async Task<ActionResult<List<FilaDeEsperaResponseDto>>> ListarFilaDeEspera(Guid alojamentoId)
        {
            return Ok(await _alojamentoService.ListarFilaDeEsperaAsync(alojamentoId));
        }
    }
}
