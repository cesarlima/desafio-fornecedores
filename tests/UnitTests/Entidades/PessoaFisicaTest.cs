using System;
using System.Collections.Generic;
using Domain.Common.ValueObjects;
using Domain.Fornecedores;
using Xunit;

namespace UnitTests.Entidades
{
    public class PessoaFisicaTest
    {
        [Fact]
        public void Deve_Adicionar_Notificacao_Quando_CPF_Invalido()
        {
            var cpf = new CPF("8907178704");
            var pessoa = new PessoaFisica("nome", "3330909", new DateTime(1990, 10, 20), cpf);
            var esperado = new List<string>() { "CPF inválido" };

            Assert.Equal(esperado, pessoa.Notificacoes);
        }

        [Fact]
        public void Deve_Adicionar_Notificacao_Quando_RG_Nao_Informado()
        {
            var cpf = new CPF("89071787044");
            var dataNascimento = new DateTime(1990, 10, 20);
            var pessoa = new PessoaFisica("nome", "", dataNascimento, cpf);
            var esperado = new List<string>() { "RG é obrigatório" };

            Assert.Equal(esperado, pessoa.Notificacoes);

            pessoa = new PessoaFisica("nome", null, dataNascimento, cpf);

            Assert.Equal(esperado, pessoa.Notificacoes);
        }

        [Fact]
        public void Deve_Adicionar_Notificacao_Quando_Data_Nascimento_Invalida()
        {
            var cpf = new CPF("89071787044");
            var pessoa = new PessoaFisica("nome", "3330909", new DateTime(), cpf);
            var esperado = new List<string>() { "Data de nascimento é obrigatório" };

            Assert.Equal(esperado, pessoa.Notificacoes);
        }

        [Fact]
        public void Deve_Adicionar_Notificacao_Quando_Nome_Nao_Informado()
        {
            var cpf = new CPF("89071787044");
            var dataNascimento = new DateTime(1990, 10, 20);
            var pessoa = new PessoaFisica("", "3330909", dataNascimento, cpf);
            var esperado = new List<string>() { "Nome é obrigatório" };

            Assert.Equal(esperado, pessoa.Notificacoes);

            pessoa = new PessoaFisica(null, "3330909", dataNascimento, cpf);

            Assert.Equal(esperado, pessoa.Notificacoes);
        }
    }
}
