using System;
using Domain.Common.ValueObjects;
using Domain.Empresas;
using Domain.Fornecedores;

namespace Infra.Entities
{
    public class EntityFactories : IEmpresaFactory, IFornecedorFactory
    {
        public Empresa NovaEmpresa(string uf, string nomeFantasia, string cnpj)
        {
            return new Empresa(uf, nomeFantasia, new CNPJ(cnpj));
        }

        public PessoaFisica NovaPessoaFisica(string nome, string rg, DateTime dataNascimento, CPF cpf)
        {
            return new PessoaFisica(nome, rg, dataNascimento, cpf);
        }

        public PessoaJuridica NovaPessoaJuridica(string nome, CNPJ cnpj)
        {
            return new PessoaJuridica(nome, cnpj);
        }

        public Fornecedor NovoFornecedor(Empresa empresa, Pessoa pessoa)
        {
            return new Fornecedor(pessoa, empresa);
        }
    }
}