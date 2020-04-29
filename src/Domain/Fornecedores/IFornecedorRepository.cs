using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Fornecedores
{
    public interface IFornecedorRepositorio
    {
        Task Salvar(Fornecedor fornecedor);
        Task<IEnumerable<Fornecedor>> ObterFornecedores(string nome, string cpfCnpj, DateTime? dataCadastro);
    }
}
