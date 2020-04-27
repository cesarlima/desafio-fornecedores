using System;
using System.Threading.Tasks;
using Domain.Common.ValueObjects;

namespace Domain.Empresas
{
    public interface IEmpresaRepositorio
    {
        Task Save(Empresa empresa);
        Task<bool> EmpresaJaCadastrada(CNPJ cnpj);
        Task<Empresa> ObterEmpresa(Guid id);
    }
}