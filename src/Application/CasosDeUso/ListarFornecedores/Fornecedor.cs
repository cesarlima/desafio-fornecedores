using System;
namespace Application.CasosDeUso.ListarFornecedores
{
    public sealed class Fornecedor
    {
        public Guid Id { get; }
        public string Nome { get; }
        public string CpfCnpj { get; }

        public Fornecedor(Guid id, string nome, string cpfCnpj)
        {
            Id = id;
            Nome = nome;
            CpfCnpj = cpfCnpj;
        }
    }
}
