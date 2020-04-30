using System;
using System.Collections.Generic;
using Domain.Common.ValueObjects;
using Domain.Fornecedores;
using Xunit;

namespace UnitTests.Entidades
{
    public class PessoaJuridicaTest
    {
        [Fact]
        public void Deve_Adicionar_Notificacao_Quando_Nome_Nao_Informado()
        {
            var cnpj = new CNPJ("64618571000177");
            var pessoa = new PessoaJuridica(null, cnpj);
            var esperado = new List<string>() { "Nome é obrigatório" };

            Assert.Equal(esperado, pessoa.Notificacoes);

            pessoa = new PessoaJuridica("", cnpj);

            Assert.Equal(esperado, pessoa.Notificacoes);
        }

        [Fact]
        public void Deve_Adicionar_Notificacao_Quando_CNPJ_Invalido()
        {
            var cnpj = new CNPJ("6461857100017");
            var pessoa = new PessoaJuridica("nome", cnpj);
            var esperado = new List<string>() { "CNPJ inválido" };

            Assert.Equal(esperado, pessoa.Notificacoes);
        }
    }
}
