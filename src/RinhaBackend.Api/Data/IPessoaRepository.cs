using RinhaBackend.Api.Models;

namespace RinhaBackend.Api.Data;

public interface IPessoaRepository
{
    Task Add(Pessoa pessoa);
    Task<IList<Pessoa>> Search(string t);
    Task<Pessoa> Get(Guid id);
    Task<int> Count();
}