using Domain.Empresas;
using Domain.Fornecedores;
using Infra.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Infra.Contexts
{
    public class FornecedoresContext : DbContext
    {
        public FornecedoresContext(DbContextOptions<FornecedoresContext> options) : base(options) { }
        public FornecedoresContext() { }

        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Fornecedor> Fornecedors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmpresaMap());
            modelBuilder.ApplyConfiguration(new PessoaMap());
            modelBuilder.ApplyConfiguration(new PessoaFisicaMap());
            modelBuilder.ApplyConfiguration(new PessoaJuridicaMap());
            modelBuilder.ApplyConfiguration(new FornecedorMap());
        }
    }
}

//https://docs.microsoft.com/pt-br/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli

//https://github.com/dotnet-architecture/eShopOnWeb/issues/94