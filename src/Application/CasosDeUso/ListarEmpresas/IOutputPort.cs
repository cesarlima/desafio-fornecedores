using System;
namespace Application.CasosDeUso.ListarEmpresas
{
    public interface IOutputPort : IOutputPortStandard<ListarEmpresaOutput>, IOutputPortNotFound, IOutputPortNotification
    {
    }
}
