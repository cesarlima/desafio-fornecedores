using System.Collections.Generic;

namespace Application.CasosDeUso.ListarFornecedores
{
    public sealed class ListarFornecedoresOutput
    {
        public IEnumerable<Fornecedor>  Fornecedores { get; }

        public ListarFornecedoresOutput(IEnumerable<Fornecedor> fornecedores)
        {
            Fornecedores = fornecedores;
        }
    }
}
