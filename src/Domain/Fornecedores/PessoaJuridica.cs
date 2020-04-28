using Domain.Common.ValueObjects;
using Domain.Fornecedores.ValueObject;

namespace Domain.Fornecedores
{
    public  class PessoaJuridica : Pessoa
    {
        public CNPJ CNPJ { get; protected set; }

        public PessoaJuridica(string nome, CNPJ cnpj)
           : base(nome)
        {
            CNPJ = cnpj;
            PessoaTipo = PessoaTipo.PessoaJuridica;
            Validar();
        }

        protected PessoaJuridica() { }

        public override string ObterNumeroCpfCnpj()
        {
            return CNPJ.ToString();
        }

        protected override void Validar()
        {
            if (CNPJ.Valido == false)
            {
                AdicionarNotificacao("CNPJ inválido");
            }
        }
    }
}
