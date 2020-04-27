using System;

namespace Domain.Fornecedores.ValueObject
{
    public class Telefone
    {
        public Guid Id { get; protected set; }
        public string Numero { get; protected set; }

        protected Telefone() { }

        public Telefone(string numero)
        {
            Id = Guid.NewGuid();
            Numero = numero ?? throw new ArgumentNullException(nameof(numero));
        }
    }
}
