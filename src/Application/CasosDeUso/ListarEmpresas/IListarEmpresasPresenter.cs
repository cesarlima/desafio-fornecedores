using System;
namespace Application.CasosDeUso.ListarEmpresas
{
    public interface IListarEmpresasPresenter : IOutputPortStandard<ListarEmpresaOutput>, IOutputPortNotFound, IOutputPortNotification
    {
    }
}
