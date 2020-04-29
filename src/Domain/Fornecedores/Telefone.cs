using System;
using Domain.Common;

namespace Domain.Fornecedores
{
    public class Telefone : Entidade
    {
        public string Numero { get; protected set; }

        protected Telefone() { }

        public Telefone(string numero)
        {
            Numero = numero;
            Validar();
        }

        protected override void Validar()
        {
            if (string.IsNullOrEmpty(Numero))
                AdicionarNotificacao("Número de telefone obrigatório");
        }
    }
}
