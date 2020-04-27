using System;
using System.Collections.Generic;

namespace Application.CasosDeUso.ListarEmpresas
{
    public sealed class ListarEmpresaOutput
    {
        public IReadOnlyCollection<Empresa> Empresas { get; }

        public ListarEmpresaOutput(List<Empresa> empresas)
        {
            Empresas = empresas;
        }
    }
}
