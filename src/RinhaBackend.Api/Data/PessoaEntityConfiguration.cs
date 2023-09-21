using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RinhaBackend.Api.Models;

namespace RinhaBackend.Api.Data;

public class PessoaEntityConfiguration : IEntityTypeConfiguration<Pessoa>
{
    public void Configure(EntityTypeBuilder<Pessoa> builder)
    {
        builder
            .HasIndex(p => p.Apelido)
            .IsUnique();
        builder.ToTable("pessoas");
        builder.Property(p => p.Id)
            .HasColumnName("id");
        builder.Property(p => p.Apelido)
            .IsRequired()
            .HasMaxLength(32)
            .HasColumnName("apelido");
        builder.Property(p => p.Nome)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("nome");
        builder.Property(p => p.Nascimento)
            .IsRequired()
            .HasColumnName("nascimento");
        builder.Property(p => p.Stack)
            .HasColumnName("stack");
    }
}