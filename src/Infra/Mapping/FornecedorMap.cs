using System;
using Domain.Fornecedores;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mapping
{
    public class FornecedorMap : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.ToTable("Fornecedor");
            builder.HasKey(x => x.Id);

            builder.Property<Guid>("PessoaId");
            builder.Property<Guid>("EmpresaId");

            builder.HasOne(x => x.Empresa)
                .WithMany()
                .HasForeignKey("EmpresaId");

            builder.HasOne(x => x.Pessoa)
                .WithMany()
                .HasForeignKey("PessoaId");
        }
    }
}
