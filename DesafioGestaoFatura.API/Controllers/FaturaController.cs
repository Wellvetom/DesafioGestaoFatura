using DesafioGestaoFatura.Application.DTO;
using DesafioGestaoFatura.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesafioGestaoFatura.API.Controllers
{
    [ApiController]
    [Route("fatura")]
    public class FaturaController : ControllerBase
    {
        private readonly FaturaService _service;

        public FaturaController(FaturaService service)
        {
            _service = service;
        }
        [HttpGet("busca-faturas")]
        public async Task<IActionResult> BuscaFaturas([FromQuery] BuscaFaturaDto busca)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var faturas = await _service.BuscarFaturas(busca);

            return Ok(faturas);
        }
        [HttpPost("criar-fatura")]
        public async Task<IActionResult> Criar(CriarFaturaDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _service.CriarFatura(dto);
            return Ok(id);
        }

        [HttpPost("{id}/adiciona-itens")]
        public async Task<IActionResult> AdicionarItem(Guid id, AdicionarItemDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.AdicionarItem(id, dto);
            return Ok("item adicionado");
        }

        [HttpPut("{id}/fechar-fatura")]
        public async Task<IActionResult> Fechar(Guid id)
        {
            await _service.FecharFatura(id);
            return Ok("fatura fechada");
        }
    }
}
