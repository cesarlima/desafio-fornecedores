using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CasosDeUso.CadastrarEmpresa;
using Application.Services;
using Domain.Common;
using Domain.Empresas;
using Moq;
using Xunit;

namespace UnitTests.CasosDeUso.CadastrarEmpresa
{
    public class CadastrarEmpresaCasoDeUsoTest
    {
        private readonly Mock<IOutputPort> _empresaPresenterMock = new Mock<IOutputPort>();
        private Mock<IEmpresaRepositorio> _empresaRepositorioMock = new Mock<IEmpresaRepositorio>();
        private Mock<IUnitOfWork> _uowMock = new Mock<IUnitOfWork>();
        private Mock<IEmpresaFactory> _empresaFactoryMock = new Mock<IEmpresaFactory>();
        private IEmpresaFactory _empresaFactory = new EntidadeFactories();

        [Fact]
        public void Deve_Disparar_ArgumentNullException_Caso_IOutputPort_For_Null()
        {
            Assert.Throws<ArgumentNullException>(() => new CadastrarEmpresaCasoDeUso(null, _empresaRepositorioMock.Object, _empresaFactory, _uowMock.Object));
        }

        [Fact]
        public void Deve_Disparar_ArgumentNullException_Caso_IEmpresaRepositorio_For_Null()
        {
            Assert.Throws<ArgumentNullException>(() => new CadastrarEmpresaCasoDeUso(_empresaPresenterMock.Object, null, _empresaFactory, _uowMock.Object));
        }

        [Fact]
        public async Task Deve_Adicionar_Notificacao_Se_CNPJ_Invalido()
        {
            var sut = CriarSUT();
            await sut.Execute(new CadastrarEmpresaInput("any_uf", "any_empresa", "888"));
            var notificacoes = new List<string>() { "CNPJ inválido" };
            _empresaPresenterMock.Verify(presenter => presenter.AddNotifications(notificacoes), Times.Once());
        }

        [Fact]
        public async Task Nao_Deve_Adicionar_Notificacao_Se_CNPJ_Valido()
        {
            var sut = CriarSUT();
            await sut.Execute(new CadastrarEmpresaInput("any_uf", "any_empresa", "33.194.965/0001-11"));

            _empresaPresenterMock.Verify(presenter => presenter.AddNotification("CNPJ inválido"), Times.Never());
        }

        [Fact]
        public async Task Deve_Adicionar_Notificacao_Se_CNPJ_Ja_Cadastrado()
        {
            var input = new CadastrarEmpresaInput("any_uf", "any_empresa", "33.194.965/0001-11");
            var sut = CriarSUT();
            _empresaRepositorioMock.Setup(rep => rep.EmpresaJaCadastrada(new Domain.Common.ValueObjects.CNPJ(input.CNPJ))).ReturnsAsync(true);

            await sut.Execute(input);

            _empresaPresenterMock.Verify(presenter => presenter.AddNotification("CNPJ já cadastrado"), Times.Once());
        }

        [Fact]
        public async Task Deve_Adicionar_CadastrarEmpresaOutput_Corretamente_Se_Empresa_Cadastrada_Com_Sucesso()
        {
            var input = new CadastrarEmpresaInput("any_uf", "any_empresa", "33.194.965/0001-11");
            var empresa = _empresaFactory.NovaEmpresa(input.UF, input.NomeFantasia, input.CNPJ);

            _empresaFactoryMock.Setup(f => f.NovaEmpresa(input.UF, input.NomeFantasia, input.CNPJ)).Returns(empresa);
            
            CadastrarEmpresaOutput result = null;
            _empresaPresenterMock.Setup(presenter => presenter.AddResult(It.IsAny<CadastrarEmpresaOutput>()))
            .Callback((CadastrarEmpresaOutput r) => result = r);

            var sut = CriarSUT(empresaFactoryMock: _empresaFactoryMock);
            await sut.Execute(input);

            Assert.Equal(empresa.Id, result?.Id);
            Assert.Equal(empresa.UF, result?.UF);
            Assert.Equal(empresa.NomeFantasia, result?.NomeFantasia);
            Assert.Equal(empresa.CNPJ.ToString(), result?.CNPJ);
        }

        private CadastrarEmpresaCasoDeUso CriarSUT(Mock<IOutputPort> empresaPresenterMock = null,
                                                   Mock<IEmpresaRepositorio> empresaRepositorioMock = null,
                                                   Mock<IUnitOfWork> uowMock = null,
                                                   Mock<IEmpresaFactory> empresaFactoryMock = null)
        {
            return new CadastrarEmpresaCasoDeUso(empresaPresenterMock?.Object ?? _empresaPresenterMock.Object,
                                                 empresaRepositorioMock?.Object ?? _empresaRepositorioMock.Object,
                                                 empresaFactoryMock?.Object ?? _empresaFactory,
                                                 uowMock?.Object ?? _uowMock.Object);
        }
    }
}
//https://github.com/Moq/moq4/wiki/Quickstart