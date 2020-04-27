using System;
namespace Application.CasosDeUso.CadastrarFornecedor
{
    public interface IOutputPort : IOutputPortStandard<CadastrarFornecedorOutput>, IOutputPortNotification, IOutputPortNotFound
    {
    }
}
