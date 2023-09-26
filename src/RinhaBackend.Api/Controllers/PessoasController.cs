using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RinhaBackend.Api.Data;
using RinhaBackend.Api.Models;

namespace RinhaBackend.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PessoasController : ControllerBase
{
    private readonly IPessoaRepository _pessoaRepository;

    public PessoasController(IPessoaRepository pessoaRepository)
    {
        _pessoaRepository = pessoaRepository;
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(Pessoa pessoa)
    {
        try
        {
            if (pessoa.Apelido is null || pessoa.Nome is null)
                return new StatusCodeResult(422);

            await _pessoaRepository.Add(pessoa);

            Response.Headers.Add("Location", $"/pessoas/{pessoa.Id}");
            
            return Created(new Uri(Request.Path), pessoa);
        }
        catch
        {
            return new StatusCodeResult(422);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Get(string t)
    {
        try
        {
            if (t is null)
                return BadRequest();

            var pessoas = await _pessoaRepository.Search(t);

            return Ok(pessoas);
        }
        catch
        {
            return Ok(new List<Pessoa>());
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        try
        {
            var pessoa = await _pessoaRepository.Get(id);

            return pessoa is null ? NotFound() : Ok(pessoa);
        }
        catch
        {
            return NotFound();
        }
    }

    [HttpGet("/contagem-pessoas")]
    public async Task<IActionResult> Count()
    {
        try
        {
            return Ok(await _pessoaRepository.Count());
        }
        catch
        {
            return BadRequest();
        }
    }
}