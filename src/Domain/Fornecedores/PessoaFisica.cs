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

        protected PessoaFisica() { }
        
        public PessoaFisica(string nome, string rg, DateTime dataNascimento, CPF cpf)
            : base(nome)
        {
            RG = rg;
            DataNascimento = dataNascimento;
            CPF = cpf;
            PessoaTipo = PessoaTipo.PessoaFisica;

            Validar();
        }

        public override string ObterNumeroCpfCnpj()
        {
            return CPF.ToString();
        }

        protected override void Validar()
        {
            if (CPF.Valido == false)
            {
                AdicionarNotificacao("CPF inválido");
            }

            if (string.IsNullOrEmpty(RG))
                AdicionarNotificacao("RG é obrigatório");

            if (DataNascimento == DateTime.MinValue)
                AdicionarNotificacao("Data de nascimento é obrigatório");
        }
    }
}
