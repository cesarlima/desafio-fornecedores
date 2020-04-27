using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CasosDeUso.CadastrarFornecedor;
using Application.Services;
using Domain.Empresas;
using Domain.Fornecedores;
using Infra.Entities;
using Moq;
using Xunit;

namespace UnitTests.CasosDeUso.CadastrarFornecedor
{
    public class CadastrarFornecedorCasodeUsoTest
    {
        private readonly Mock<IOutputPort> _outputPortMock = new Mock<IOutputPort>();
        private readonly Mock<IFornecedorFactory> _pessoaFactoryMock = new Mock<IFornecedorFactory>();
        private readonly Mock<IFornecedorRepositorio> _fornecedorRepositorioMock = new Mock<IFornecedorRepositorio>();
        private readonly Mock<IEmpresaRepositorio> _empresaRepositorioMock = new Mock<IEmpresaRepositorio>();
        private Mock<IUnitOfWork> _uow = new Mock<IUnitOfWork>();
        private readonly IFornecedorFactory _pessoaFactory = new EntityFactories();

        [Fact]
        public async Task Deve_Adicionar_Notificacao_Se_Fornecedor_Pessoa_Fisica_Sem_Rg_E_Sem_Data_Nascimento()
        {
            var sut = CriarSUT();
            var input = CriarCadastrarFornecedorInput(pessoaJuridica: false, rg: null, dataNascimento: null);
            await sut.Execute(input);

            _outputPortMock.Verify(presenter => presenter.AddNotification("Fornecedor pessoa física deve informar o RG"), Times.Once());
            _outputPortMock.Verify(presenter => presenter.AddNotification("Fornecedor pessoa física deve informar a data de nascimento"), Times.Once());
        }

        [Fact]
        public async Task Deve_Adicionar_Notificacao_Caso_Empresa_Do_Parana_Com_Fornecedor_Pessoa_Fisica_Menor_De_Idade()
        {
            var sut = CriarSUT();
            var input = CriarCadastrarFornecedorInput(pessoaJuridica: false, dataNascimento: new DateTime(2005, 3, 12));
            await sut.Execute(input);

            _outputPortMock.Verify(presenter => presenter.AddNotification("Não é permitido cadastrar fornecedor pessoa física menor de idade"), Times.Once());
        }

        [Fact]
        public async Task Deve_Adicionar_Notificacao_Se_CNPJ_Invalido()
        {
            var sut = CriarSUT();
            var input = CriarCadastrarFornecedorInput(cpfCnpj: "1167289700011");
            await sut.Execute(input);

            _outputPortMock.Verify(presenter => presenter.AddNotification("CNPJ inválido"), Times.Once());
        }

        [Fact]
        public async Task Deve_Adicionar_Notificacao_Se_CPF_Invalido()
        {
            var sut = CriarSUT();
            var input = CriarCadastrarFornecedorInput(pessoaJuridica: false, cpfCnpj: "1167289700011", dataNascimento: new DateTime(1990, 3, 12));
            await sut.Execute(input);

            _outputPortMock.Verify(presenter => presenter.AddNotification("CPF inválido"), Times.Once());
        }

        [Fact]
        public async Task Deve_Adicionar_Result_Corretamente()
        {
            var input = CriarCadastrarFornecedorInput();
            var pessoaJuridica = _pessoaFactory.NovaPessoaJuridica(input.Nome, new Domain.Common.ValueObjects.CNPJ(input.CpfCnpj));
            var outputMock = new CadastrarFornecedorOutput(pessoaJuridica.Id, pessoaJuridica.Nome, pessoaJuridica.CNPJ.ToString(), pessoaJuridica.DataCadastro);
            var sut = CriarSUT();

            _pessoaFactoryMock.Setup(f => f.NovaPessoaJuridica(input.Nome, new Domain.Common.ValueObjects.CNPJ(input.CpfCnpj))).Returns(pessoaJuridica);
            CadastrarFornecedorOutput result = null;
            _outputPortMock.Setup(presenter => presenter.AddResult(It.IsAny<CadastrarFornecedorOutput>()))
           .Callback((CadastrarFornecedorOutput r) => result = r);

            await sut.Execute(input);

            Assert.NotNull(result);
        }



        private CadastrarFornecedorCasoDeUso CriarSUT()
        {
            return new CadastrarFornecedorCasoDeUso(_outputPortMock.Object,
                                                    _pessoaFactory,
                                                    _empresaRepositorioMock.Object,
                                                    _fornecedorRepositorioMock.Object,
                                                    _uow.Object);
        }

        private CadastrarFornecedorInput CriarCadastrarFornecedorInput(Guid empresaId = new Guid(),
                                                                      string fornecedorNome = "Fornecedor",
                                                                      string cpfCnpj = "11672897000116",
                                                                      string uf = "PR",
                                                                      string rg = "141285771",
                                                                      List<string> telefones = null,
                                                                      bool pessoaJuridica = true,
                                                                      DateTime? dataNascimento = new DateTime?())
        {
            return new CadastrarFornecedorInput(empresaId, fornecedorNome, cpfCnpj, uf, rg, telefones, pessoaJuridica, dataNascimento);
        }
    }
}
