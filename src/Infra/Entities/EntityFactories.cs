using System;
using Domain.Common.ValueObjects;
using Domain.Empresas;

namespace Infra.Entities
{
    public class EntityFactories : IEmpresaFactory
    {
        public Domain.Empresas.Empresa NovaEmpresa(string uf, string nomeFantasia, Documento cnpj)
        {
            return new Domain.Empresas.Empresa(uf, nomeFantasia, cnpj);
        }
    }
}