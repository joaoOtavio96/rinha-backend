using Microsoft.EntityFrameworkCore;
using RinhaBackend.Api.Models;

namespace RinhaBackend.Api.Data;

public class PessoaEntityRepository : IPessoaRepository
{
    private readonly ApiDbContext _context;

    public PessoaEntityRepository(ApiDbContext context)
    {
        _context = context;
    }

    public async Task Add(Pessoa pessoa)
    {
        await _context.Pessoas.AddAsync(pessoa);
        await _context.SaveChangesAsync();
    }

    public async Task<IList<Pessoa>> Search(string t)
    {
        return await _context.Pessoas.Where(p =>
                p.Apelido.ToUpper().Contains(t.ToUpper()) ||
                p.Nome.ToUpper().Contains(t.ToUpper()) ||
                p.Stack.Any(s => s.ToUpper().Contains(t.ToUpper()))
            )
            .Take(50)
            .ToListAsync();
    }

    public async Task<Pessoa> Get(Guid id)
    {
        return await _context.Pessoas.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<int> Count()
    {
        return await _context.Pessoas.CountAsync();
    }
}