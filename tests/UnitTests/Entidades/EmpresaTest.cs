using System.Collections.Generic;
using Domain.Common.ValueObjects;
using Domain.Empresas;
using Xunit;

namespace UnitTests.Entidades
{
    public class EmpresaTest
    {
        [Fact]
        public void Deve_Adicionar_Notificacao_Quando_Nome_Fantasia_Nao_Informado()
        {
            var cnpj = new CNPJ("11672897000116");
            var sut = new Empresa("any_uf", null, cnpj);
            var esperado = new List<string>() { "Nome fantasia é obrigatório" };

            Assert.Equal(esperado, sut.Notificacoes);

            sut = new Empresa("UF", "", cnpj);

            Assert.Equal(esperado, sut.Notificacoes);
        }

        [Fact]
        public void Deve_Adicionar_Notificacao_Quando_UF_Nao_Informado()
        {
            var cnpj = new CNPJ("11672897000116");
            var sut = new Empresa("", "Fantasia", cnpj);
            var esperado = new List<string>() { "UF é obrigatório" };

            Assert.Equal(esperado, sut.Notificacoes);

            sut = new Empresa(null, "Fantasia", cnpj);

            Assert.Equal(esperado, sut.Notificacoes);
        }

        [Fact]
        public void Deve_Adicionar_Notificacao_Quando_CNPJ_Invalido()
        {
            var cnpj = new CNPJ("116728970001");
            var sut = new Empresa("UF", "Fantasia", cnpj);
            var esperado = new List<string>() { "CNPJ inválido" };

            Assert.Equal(esperado, sut.Notificacoes);
        }
    }
}
