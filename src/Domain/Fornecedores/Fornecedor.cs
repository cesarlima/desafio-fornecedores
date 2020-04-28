using System;
using Domain.Common;
using Domain.Empresas;

namespace Domain.Fornecedores
{
    public class Fornecedor : Entidade
    {
        public Pessoa Pessoa { get; protected set; }

        public Empresa Empresa { get; protected set; }

        protected Fornecedor() { }

        public Fornecedor(Pessoa pessoa, Empresa empresa)
        {
            Id = Guid.NewGuid();
            Pessoa = pessoa ?? throw new ArgumentNullException(nameof(pessoa));
            Empresa = empresa ?? throw new ArgumentNullException(nameof(empresa));
            Validar();
        }

        protected override void Validar()
        {
            AdicionarNotificacoes(Pessoa.Notificacoes);
            AdicionarNotificacoes(Empresa.Notificacoes);
            ValidarFornecedorPessoaFisicaDoParana();
        }

        private void ValidarFornecedorPessoaFisicaDoParana()
        {
            if (Empresa.UF.Equals("PR") && Pessoa is PessoaFisica pessoaFisica)
            {
                var menorDeIdade = (DateTime.Now.Subtract(pessoaFisica.DataNascimento).TotalDays / 365.2425) < 18;

                if (menorDeIdade)
                    AdicionarNotificacao("Não é permitido cadastrar fornecedor pessoa física menor de idade");
            }
        }
    }
}
