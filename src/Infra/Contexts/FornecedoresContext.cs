using Domain.Empresas;
using Domain.Fornecedores;
using Infra.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Infra.Contexts
{
    public class FornecedoresContext : DbContext
    {
        public FornecedoresContext(DbContextOptions<FornecedoresContext> options) : base(options) { }

        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<PessoaFisica> PessoasFisicas { get; set; }
        public DbSet<PessoaJuridica> PessoasJuridicas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmpresaMap());
            modelBuilder.ApplyConfiguration(new PessoaMap());
            modelBuilder.ApplyConfiguration(new PessoaFisicaMap());
            modelBuilder.ApplyConfiguration(new PessoaJuridicaMap());
            modelBuilder.ApplyConfiguration(new FornecedorMap());
            modelBuilder.ApplyConfiguration(new TelefoneMap());
        }
    }
}
