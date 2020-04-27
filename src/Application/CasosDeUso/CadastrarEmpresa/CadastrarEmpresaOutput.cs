using System;

namespace Application.CasosDeUso.CadastrarEmpresa
{
    public sealed class CadastrarEmpresaOutput
    {
        public Guid Id { get; }
        public string UF { get; }
        public string NomeFantasia { get; }
        public string CNPJ { get; }

        public CadastrarEmpresaOutput(Guid id, string uf, string nomeFantasia, string cnpj)
        {
            this.Id = id;
            this.UF = uf;
            this.NomeFantasia = nomeFantasia;
            this.CNPJ = cnpj;
        }
    }
}