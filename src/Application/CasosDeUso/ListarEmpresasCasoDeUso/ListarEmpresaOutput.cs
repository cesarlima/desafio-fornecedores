using System;
using System.Collections.Generic;

namespace Application.CasosDeUso.ListarEmpresasCasoDeUso
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
