using Dapper;
using Dapper.Contrib.Extensions;
using Npgsql;
using RinhaBackend.Api.Models;

namespace RinhaBackend.Api.Data;

public class PessoaDapperRepository : IPessoaRepository
{
    private readonly string _connectionString;

    public PessoaDapperRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task Add(Pessoa pessoa)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        pessoa.Id = Guid.NewGuid();
        var sql = @"INSERT INTO Pessoas (Id, Apelido, Nome, Nascimento, Stack) 
                    VALUES (@id, @Apelido, @Nome, @Nascimento, @Stack)";
        await connection.ExecuteAsync(sql, pessoa);
    }

    public async Task<IList<Pessoa>> Search(string t)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"SELECT * FROM Pessoas WHERE
                    Apelido ILIKE @term OR
                    Nome ILIKE @term OR
                    (SELECT EXISTS (SELECT 1 FROM unnest(stack) n WHERE n ~~* @term))
                    LIMIT(50)";
        var pessoas = await connection.QueryAsync<Pessoa>(sql, new { term = $"%{t}%" });

        return pessoas.ToList();
    }

    public async Task<Pessoa> Get(Guid id)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        var sql = "SELECT * FROM Pessoas WHERE id = @id";
        
        return await connection.QueryFirstOrDefaultAsync<Pessoa>(sql, new { id });
    }

    public async Task<int> Count()
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        var sql = "SELECT COUNT(*) FROM Pessoas";

        return await connection.ExecuteScalarAsync<int>(sql);
    }
}