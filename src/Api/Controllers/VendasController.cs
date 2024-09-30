using Domain;
using Microsoft.AspNetCore.Mvc;
using Domain.Repositorios.Interfaces;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VendasController : ControllerBase
{
    private readonly IVendaRepository _vendaRepository;

    public VendasController(IVendaRepository vendaRepository)
    {
        _vendaRepository = vendaRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var vendas = await _vendaRepository.ObterTodasAsync();

            return Ok(vendas);
        }
        catch (Exception e)
        {
            return Problem(statusCode: 500, detail: e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        try
        {
            var venda = await _vendaRepository.ObterPorIdAsync(id);

            if (venda == null) return NotFound();

            return Ok(venda);
        }
        catch (Exception e)
        {
            return Problem(statusCode: 500, detail: e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Venda venda)
    {
        try
        {
            if (venda is null) return BadRequest();

            await _vendaRepository.AdicionarAsync(venda);

            return CreatedAtAction(nameof(Get), new { id = venda.Id }, venda);
        }
        catch (Exception e)
        {
            return Problem(statusCode: 500, detail: e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] Venda venda)
    {
        try
        {
            if (id != venda.Id) return BadRequest();

            if (venda is null) return BadRequest();

            await _vendaRepository.AtualizarAsync(venda);

            return NoContent();
        }
        catch (Exception e)
        {
            return Problem(statusCode: 500, detail: e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var venda = await _vendaRepository.ObterPorIdAsync(id);

        if (venda == null) return NotFound();

        await _vendaRepository.ExcluirAsync(id);

        return NoContent();
    }
}
