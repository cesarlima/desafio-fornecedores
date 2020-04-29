using System;
namespace Application.CasosDeUso.ListarFornecedores
{
    public sealed class ListarFornecedoresInput
    {
        public string Nome { get; }
        public string CpfCnpj { get; }
        public DateTime? DataCadastro { get; }

        public ListarFornecedoresInput(string nome, string cpfCnpj, DateTime? dataCadastro)
        {
            Nome = nome;
            CpfCnpj = cpfCnpj;
            DataCadastro = dataCadastro;
        }
    }
}
