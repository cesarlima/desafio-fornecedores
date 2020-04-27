using System;
using Domain.Common.ValueObjects;

namespace Domain.Empresas
{
    public interface IEmpresa
    {
        Guid Id { get; }
        string UF { get; }
        string NomeFantasia { get; }
        Documento CNPJ { get; }
    }
}