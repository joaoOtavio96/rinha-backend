using Microsoft.EntityFrameworkCore;
using RinhaBackend.Api.Models;

namespace RinhaBackend.Api.Data;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }
    
    public virtual DbSet<Pessoa> Pessoas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pessoa>()
            .HasIndex(p => p.Apelido)
            .IsUnique();
        
        modelBuilder.Entity<Pessoa>(entity =>
        {
            entity.ToTable("pessoas");
            entity.Property(p => p.Id)
                .HasColumnName("id");
            entity.Property(p => p.Apelido)
                .IsRequired()
                .HasMaxLength(32)
                .HasColumnName("apelido");
            entity.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("nome");
            entity.Property(p => p.Nascimento)
                .IsRequired()
                .HasColumnName("nascimento");
            entity.Property(p => p.Stack)
                .HasColumnName("stack");
        });
    }
}