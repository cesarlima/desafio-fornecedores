//using Infra.Entities;
using Domain.Fornecedores;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mapping
{
    public class PessoaJuridicaMap : IEntityTypeConfiguration<PessoaJuridica>
    {
        public void Configure(EntityTypeBuilder<PessoaJuridica> builder)
        {
            builder.Property(x => x.CNPJ)
                .HasConversion(c => c.ToString(), c => new Domain.Common.ValueObjects.CNPJ(c))
                .HasColumnType("varchar(14)");
        }
    }
}
