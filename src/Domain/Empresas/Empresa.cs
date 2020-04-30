using Domain.Common;
using Domain.Common.ValueObjects;

namespace Domain.Empresas
{
    public class Empresa : Entidade
    {
        public string UF { get; protected set; }
        public string NomeFantasia { get; protected set; }
        public CNPJ CNPJ { get; protected set; }

        protected Empresa()
        {
        }

        public Empresa(string uf, string nomeFantasia, CNPJ cnpj)
        {
            UF = uf;
            NomeFantasia = nomeFantasia;
            CNPJ = cnpj;

            Validar();
        }

        public override bool Equals(object obj)
        {
            return obj is Empresa empresa &&
                   Id.Equals(empresa.Id);
        }

        public override int GetHashCode() => 2108858624 + Id.GetHashCode();

        protected override void Validar()
        {
            if (CNPJ.Valido == false)
                AdicionarNotificacao("CNPJ inválido");

            if (string.IsNullOrEmpty(NomeFantasia))
                AdicionarNotificacao("Nome fantasia é obrigatório");

            if (string.IsNullOrEmpty(UF))
                AdicionarNotificacao("UF é obrigatório");
        }
    }
}