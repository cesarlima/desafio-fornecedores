using System;
using System.Threading.Tasks;
using Application.CasosDeUso.CadastrarEmpresa;
using Application.Services;
using Domain.Empresas;
using Infra.Entities;
using Moq;
using Xunit;

namespace UnitTests.CasosDeUso.CadastrarEmpresa
{
    public class CadastrarEmpresaCasoDeUsoTest
    {
        private readonly Mock<IOutputPort> _empresaPresenterMock = new Mock<IOutputPort>();
        private Mock<IEmpresaRepositorio> _empresaRepositorioMock = new Mock<IEmpresaRepositorio>();
        private Mock<IUnitOfWork> _uow = new Mock<IUnitOfWork>();
        private IEmpresaFactory _empresaFactory = new EntityFactories();


        [Fact]
        public void Deve_Disparar_ArgumentNullException_Caso_IOutputPort_For_Null()
        {
            Assert.Throws<ArgumentNullException>(() => new CadastrarEmpresaCasoDeUso(null, _empresaRepositorioMock.Object, _empresaFactory, _uow.Object));
        }

        [Fact]
        public void Deve_Disparar_ArgumentNullException_Caso_IEmpresaRepositorio_For_Null()
        {
            Assert.Throws<ArgumentNullException>(() => new CadastrarEmpresaCasoDeUso(_empresaPresenterMock.Object, null, _empresaFactory, _uow.Object));
        }

        [Fact]
        public async Task Deve_Adicionar_Notificacao_Se_CNPJ_Invalido()
        {
            var sut = new CadastrarEmpresaCasoDeUso(_empresaPresenterMock.Object, _empresaRepositorioMock.Object, _empresaFactory, _uow.Object);
            await sut.Execute(new CadastrarEmpresaInput("any_uf", "any_empresa", "888"));

            _empresaPresenterMock.Verify(presenter => presenter.AddNotification("CNPJ inválido"), Times.Once());
        }

        [Fact]
        public async Task Nao_Deve_Adicionar_Notificacao_Se_CNPJ_Valido()
        {
            var sut = new CadastrarEmpresaCasoDeUso(_empresaPresenterMock.Object, _empresaRepositorioMock.Object, _empresaFactory, _uow.Object);
            await sut.Execute(new CadastrarEmpresaInput("any_uf", "any_empresa", "33.194.965/0001-11"));

            _empresaPresenterMock.Verify(presenter => presenter.AddNotification("CNPJ inválido"), Times.Never());
        }

        [Fact]
        public async Task Deve_Adicionar_Notificacao_Se_CNPJ_Ja_Cadastrado()
        {
            const string cnpj = "33.194.965/0001-11";
            var sut = new CadastrarEmpresaCasoDeUso(_empresaPresenterMock.Object, _empresaRepositorioMock.Object, _empresaFactory, _uow.Object);
            _empresaRepositorioMock.Setup(rep => rep.EmpresaJaCadastrada(cnpj)).ReturnsAsync(true);
            await sut.Execute(new CadastrarEmpresaInput("any_uf", "any_empresa", cnpj));

            _empresaPresenterMock.Verify(presenter => presenter.AddNotification("CNPJ já cadastrado"), Times.Once());
        }

        [Fact]
        public async Task Deve_Adicionar_Result_Se_CNPJ_Ja_Cadastrado()
        {
            const string cnpj = "33.194.965/0001-11";
            var sut = new CadastrarEmpresaCasoDeUso(_empresaPresenterMock.Object, _empresaRepositorioMock.Object, _empresaFactory, _uow.Object);
            CadastrarEmpresaOutput result = null;
            _empresaPresenterMock.Setup(presenter => presenter.AddResult(It.IsAny<CadastrarEmpresaOutput>()))
            .Callback((CadastrarEmpresaOutput r) => result = r);

            await sut.Execute(new CadastrarEmpresaInput("any_uf", "any_empresa", cnpj));

            Assert.NotNull(result);
        }
    }
}
//https://github.com/Moq/moq4/wiki/Quickstart