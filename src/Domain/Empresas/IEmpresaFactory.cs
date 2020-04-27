using Domain.Common.ValueObjects;

namespace Domain.Empresas
{
    public interface IEmpresaFactory
    {
         Empresa NovaEmpresa(string uf, string nomeFantasia, Documento cnpj);
    }
}