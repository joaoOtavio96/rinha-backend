using Microsoft.EntityFrameworkCore;
using RinhaBackend.Api.Models;

namespace RinhaBackend.Api.Data;

public class ApiDbContext : DbContext
{
    static ApiDbContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }
    
    public virtual DbSet<Pessoa> Pessoas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PessoaEntityConfiguration());
    }
}