using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Common.ValueObjects;

namespace Domain.Fornecedores
{
    public interface IFornecedorRepositorio
    {
        Task Salvar(Fornecedor fornecedor);
        Task<IEnumerable<Fornecedor>> ObterFornecedores(string nome, string cpfCnpj, DateTime? dataCadastro);
        Task<bool> PessoaJuridicaCadastrada(CNPJ cnpj);
        Task<bool> PessoaFisicaCadastrada(CPF cpf);
    }
}
