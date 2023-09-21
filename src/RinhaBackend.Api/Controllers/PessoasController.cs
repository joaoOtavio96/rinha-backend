using Microsoft.AspNetCore.Mvc;
using RinhaBackend.Api.Data;
using RinhaBackend.Api.Models;

namespace RinhaBackend.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PessoasController : ControllerBase
{
    private readonly ApiDbContext _context;

    public PessoasController(ApiDbContext context)
    {
        _context = context;
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(Pessoa pessoa)
    {
        try
        {
            if (pessoa.Apelido is null || pessoa.Nome is null)
                return new StatusCodeResult(422);
            
            _context.Pessoas.Add(pessoa);
            _context.SaveChanges();

            Response.Headers.Add("Location", $"/pessoas/{pessoa.Id}");
            
            return Ok();
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
            
            var pessoas = _context.Pessoas.Where(p =>
                    p.Apelido.ToUpper().Contains(t.ToUpper()) ||
                    p.Nome.ToUpper().Contains(t.ToUpper()) ||
                    p.Stack.Any(s => s.ToUpper().Contains(t.ToUpper()))
                )
                .Take(50)
                .ToList();

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
            var pessoa = _context.Pessoas.FirstOrDefault(p => p.Id == id);

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
            return Ok(_context.Pessoas.Count());
        }
        catch
        {
            return BadRequest();
        }
    }
}