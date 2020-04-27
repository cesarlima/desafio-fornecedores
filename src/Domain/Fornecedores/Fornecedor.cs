using System;
using Domain.Empresas;

namespace Domain.Fornecedores
{
    public class Fornecedor
    {
        public Guid Id { get; protected set; }

        public Pessoa Pessoa { get; protected set; }

        public Empresa Empresa { get; protected set; }

        protected Fornecedor() { }

        public Fornecedor(Pessoa pessoa, Empresa empresa)
        {
            Id = Guid.NewGuid();
            Pessoa = pessoa ?? throw new ArgumentNullException(nameof(pessoa));
            Empresa = empresa ?? throw new ArgumentNullException(nameof(empresa));
        }
    }
}
