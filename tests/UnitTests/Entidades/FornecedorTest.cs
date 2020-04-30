using System;
using System.Collections.Generic;
using Domain.Common.ValueObjects;
using Domain.Empresas;
using Domain.Fornecedores;
using Xunit;

namespace UnitTests.Entidades
{
    public class FornecedorTest
    {
        [Fact]
        public void Deve_Adicionar_Notificacao_Quando_Fornecedor_Pessoa_Fisica_Do_Parana_Menor_De_Idade()
        {
            var empresa = new Empresa("PR", "nome", new CNPJ("11672897000116"));
            var pessoaFisica = new PessoaFisica("nome", "rg", new DateTime(2003, 5, 8), new CPF("52647680051"));
            var sut = new Fornecedor(pessoaFisica, empresa);

            var esperado = new List<string>() { "Não é permitido cadastrar fornecedor pessoa física menor de idade" };

            Assert.Equal(esperado, sut.Notificacoes);
        }
    }
}
