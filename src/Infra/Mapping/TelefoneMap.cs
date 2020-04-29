using Domain.Fornecedores;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mapping
{
    public class TelefoneMap : IEntityTypeConfiguration<Telefone>
    {
        public void Configure(EntityTypeBuilder<Telefone> builder)
        {
            builder.ToTable("Telefone");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Numero)
                .HasColumnType("varchar(20)")
                .IsRequired();
        }
    }
}
