

using global::VendasAPI.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using VendasAPI.Domain.Dtos;

namespace VendasAPI.API.Controllers
{
    [ApiController]
    [Route("api/vendas")]
    public class VendaController : ControllerBase
    {
        private readonly IVendaService _vendaService;

        public VendaController(IVendaService vendaService)
        {
            _vendaService = vendaService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] VendaDto vendaDto)
        {
            var venda = _vendaService.Create(vendaDto);
            return CreatedAtAction(nameof(GetById), new { id = venda.VendaId }, venda);
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_vendaService.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id) => Ok(_vendaService.GetById(id));

        [HttpPut("atualizar/{id}")]
        public IActionResult Update(int id, [FromBody] VendaDto vendaDto)
        {
            _vendaService.Update(id, vendaDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Cancel(int id)
        {
            _vendaService.Cancel(id);
            return NoContent();
        }
    }
}

