using System;
using Domain.Fornecedores;
using Domain.Fornecedores.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mapping
{
    public class PessoaMap : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.ToTable("Pessoa");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
               .HasColumnType("varchar(60)")
               .IsRequired();

            var navigation = builder.Metadata.FindNavigation(nameof(Pessoa.Telefones));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasDiscriminator(x => x.PessoaTipo)
                .HasValue<PessoaFisica>(PessoaTipo.PessoaFisica)
                .HasValue<PessoaJuridica>(PessoaTipo.PessoaJuridica);
        }
    }
}
