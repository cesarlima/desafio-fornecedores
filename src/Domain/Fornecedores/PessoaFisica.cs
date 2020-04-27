using System;
using Domain.Common.ValueObjects;
using Domain.Fornecedores.ValueObject;

namespace Domain.Fornecedores
{
    public  class PessoaFisica : Pessoa
    {
        public string RG { get; protected set; }

        public DateTime DataNascimento { get; protected set;  }

        public CPF CPF  { get; protected set; }

        protected PessoaFisica()
        {

        }

        public PessoaFisica(string nome, string rg, DateTime dataNascimento, CPF cpf)
            : base(nome)
        {
            RG = rg ?? throw new ArgumentNullException(nameof(rg));
            DataNascimento = dataNascimento;
            CPF = cpf;
            PessoaTipo = PessoaTipo.PessoaFisica;
        }
    }
}
