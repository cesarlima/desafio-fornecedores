using System;
using System.Collections.Generic;
using Domain.Common.ValueObjects;
using Domain.Empresas;
using Domain.Fornecedores;

namespace Domain.Common
{
    public class EntidadeFactories : IEmpresaFactory, IFornecedorFactory
    {
        public Empresa NovaEmpresa(string uf, string nomeFantasia, string cnpj)
        {
            return new Empresa(uf, nomeFantasia, new CNPJ(cnpj));
        }

        public PessoaFisica NovaPessoaFisica(string nome, string rg, DateTime? dataNascimento, string cpf, IEnumerable<string> telefones)
        {
            var nascimento = dataNascimento != null ? dataNascimento.Value : DateTime.MinValue;
            var pessoa = new PessoaFisica(nome, rg, nascimento, new CPF(cpf));

            if (telefones != null)
            {
                foreach (var numero in telefones)
                    pessoa.AdicionarTelefone(new Telefone(numero));
            }

            return pessoa;
        }

        public PessoaJuridica NovaPessoaJuridica(string nome, string cnpj, IEnumerable<string> telefones)
        {
            var pessoa = new PessoaJuridica(nome, new CNPJ(cnpj));

            if (telefones != null)
            {
                foreach (var numero in telefones)
                    pessoa.AdicionarTelefone(new Telefone(numero));
            }
            
            return pessoa;
        }

        public Fornecedor NovoFornecedor(Empresa empresa, Pessoa pessoa)
        {
            return new Fornecedor(pessoa, empresa);
        }
    }
}
