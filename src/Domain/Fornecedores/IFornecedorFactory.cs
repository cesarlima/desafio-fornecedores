using System;
using Domain.Common.ValueObjects;
using Domain.Empresas;

namespace Domain.Fornecedores
{
    public interface IFornecedorFactory
    {
        Fornecedor NovoFornecedor(Empresa empresa, Pessoa pessoa);
        PessoaFisica NovaPessoaFisica(string nome, string rg, DateTime? dataNascimento, string cpf);
        PessoaJuridica NovaPessoaJuridica(string nome, string cnpj);
    }
}
