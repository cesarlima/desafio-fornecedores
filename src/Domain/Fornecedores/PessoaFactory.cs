using System;
using Domain.Common.ValueObjects;

namespace Domain.Fornecedores
{
    public class PessoaFactory : IPessoaFactory
    {
        public PessoaFisica NovaPessoaFisica(string nome, string rg, DateTime dataNascimento, CPF cpf)
        {
            return new PessoaFisica(nome, rg, dataNascimento, cpf);
        }

        public PessoaJuridica NovaPessoaJuridica(string nome, CNPJ cnpj)
        {
            return new PessoaJuridica(nome, cnpj);
        }
    }
}
