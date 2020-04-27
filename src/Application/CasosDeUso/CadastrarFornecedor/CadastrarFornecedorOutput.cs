using System;
namespace Application.CasosDeUso.CadastrarFornecedor
{
    public sealed class CadastrarFornecedorOutput
    {
        public Guid Id { get; }
        public string Nome { get; }
        public string CpfCnpj { get; }
        public DateTime DataCadastro { get; }

        public CadastrarFornecedorOutput(Guid id, string nome, string cpfCnpj, DateTime dataCadastro)
        {
            Id = id;
            Nome = nome;
            CpfCnpj = cpfCnpj;
            DataCadastro = dataCadastro;
        }
    }
}
