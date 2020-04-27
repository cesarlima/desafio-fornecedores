using Domain.Empresas;

namespace Domain.Fornecedores
{
    public interface IFornecedorFactory
    {
        Fornecedor NovoFornecedor(Empresa empresa, Pessoa pessoa);
    }
}
