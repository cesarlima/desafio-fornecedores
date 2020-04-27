//using Infra.Entities;
using Domain.Fornecedores;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mapping
{
    public class PessoaFisicaMap : IEntityTypeConfiguration<PessoaFisica>
    {
        public void Configure(EntityTypeBuilder<PessoaFisica> builder)
        {
            builder.Property(x => x.CPF)
                .HasConversion(c => c.ToString(), c => new Domain.Common.ValueObjects.CPF(c))
                .HasColumnType("varchar(11");
        }
    }
}
