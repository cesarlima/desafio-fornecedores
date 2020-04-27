using System;
using System.Threading.Tasks;

namespace Domain.Fornecedores
{
    public interface IFornecedorRepositorio
    {
        Task Salvar(Fornecedor fornecedor);
    }
}
