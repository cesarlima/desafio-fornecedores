using System;
using Domain.Common.ValueObjects;
using Domain.Empresas;

namespace Domain.Fornecedores
{
    public interface IFornecedorFactory
    {
        Fornecedor NovoFornecedor(Empresa empresa, Pessoa pessoa);
        PessoaFisica NovaPessoaFisica(string nome, string rg, DateTime dataNascimento, CPF cpf);
        PessoaJuridica NovaPessoaJuridica(string nome, CNPJ cnpj);
    }
}
