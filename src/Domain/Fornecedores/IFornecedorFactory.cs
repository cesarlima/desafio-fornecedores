using System;
using System.Collections.Generic;
using Domain.Empresas;

namespace Domain.Fornecedores
{
    public interface IFornecedorFactory
    {
        Fornecedor NovoFornecedor(Empresa empresa, Pessoa pessoa);
        PessoaFisica NovaPessoaFisica(string nome, string rg, DateTime? dataNascimento, string cpf, IEnumerable<string> telefones);
        PessoaJuridica NovaPessoaJuridica(string nome, string cnpj, IEnumerable<string> telefones);
    }
}
