using System.Collections.Generic;
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

        public override bool Equals(object obj)
        {
            return base.Equals(obj)
                && obj is Telefone telefone
                &&  Numero == telefone.Numero;
        }

        public override int GetHashCode()
        {
            int hashCode = -984159805;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Numero);
            return hashCode;
        }
    }
}
