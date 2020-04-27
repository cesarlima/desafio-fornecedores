using Domain.Common.ValueObjects;
using Domain.Empresas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mapping
{
    public class EmpresaMap : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.ToTable("Empresa");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.NomeFantasia)
            .HasColumnType("varchar(60)")
            .IsRequired();

            builder.Property(x => x.UF)
            .HasColumnType("varchar(2)")
            .IsRequired();

            builder.Property(x => x.CNPJ)
                .HasConversion(c => c.Numero, c => new Documento(c))
                .HasColumnName("CNPJ")
                .HasColumnType("varchar(14)");
        }
    }
}