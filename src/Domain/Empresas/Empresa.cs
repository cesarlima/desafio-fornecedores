using System;
using Domain.Common.ValueObjects;

namespace Domain.Empresas
{
    public class Empresa
    {
        public Guid Id { get; protected set; }
        public string UF { get; protected set; }
        public string NomeFantasia { get; protected set; }
        public CNPJ CNPJ { get; protected set; }

        protected Empresa()
        {
        }

        public Empresa(string uf, string nomeFantasia, CNPJ cnpj)
        {
            this.Id = Guid.NewGuid();
            this.UF = uf;
            this.NomeFantasia = nomeFantasia;
            this.CNPJ = cnpj;
        }

        public override bool Equals(object obj)
        {
            return obj is Empresa empresa &&
                   Id.Equals(empresa.Id);
        }

        public override int GetHashCode() => 2108858624 + Id.GetHashCode();
    }
}