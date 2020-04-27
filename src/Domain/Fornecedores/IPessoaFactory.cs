using System;
using Domain.Common.ValueObjects;

namespace Domain.Fornecedores
{
    public interface IPessoaFactory
    {
        PessoaFisica NovaPessoaFisica(string nome, string rg, DateTime dataNascimento, CPF cpf);
        PessoaJuridica NovaPessoaJuridica(string nome, CNPJ cnpj);
    }
}
