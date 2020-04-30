using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CasosDeUso.CadastrarFornecedor;
using Application.Services;
using Domain.Common;
using Domain.Common.ValueObjects;
using Domain.Empresas;
using Domain.Fornecedores;
using Moq;
using Xunit;

namespace UnitTests.CasosDeUso.CadastrarFornecedor
{
    public class CadastrarFornecedorCasodeUsoTest
    {
        private readonly Mock<IOutputPort> _outputPortMock = new Mock<IOutputPort>();
        private readonly Mock<IFornecedorRepositorio> _fornecedorRepositorioMock = new Mock<IFornecedorRepositorio>();
        private readonly Mock<IEmpresaRepositorio> _empresaRepositorioMock = new Mock<IEmpresaRepositorio>();
        private readonly Mock<IUnitOfWork> _uowMock = new Mock<IUnitOfWork>();
        private readonly Mock<IFornecedorFactory> _fornecedorFactoryMock = new Mock<IFornecedorFactory>();
        private readonly IFornecedorFactory _fornecedorFactory = new EntidadeFactories();
        private readonly IEmpresaFactory _empresaFactory = new EntidadeFactories();

        [Fact]
        public async Task Deve_Adicionar_Notificacao_Quando_Fornecedor_Pessoa_Fisica_Sem_RG()
        {
            var sut = CriarSUT();
            var empresa = CriarEmpresa();
            
            var input = CriarCadastrarFornecedorInput(pessoaJuridica: false, rg: null, cpfCnpj: "130.935.460-01", dataNascimento: new DateTime(1990, 3, 12));
            _empresaRepositorioMock.Setup(x => x.ObterEmpresa(input.EmpresaId)).Returns(Task.FromResult(empresa));
            await sut.Execute(input);

            var notificacoes = new List<string>() { "RG é obrigatório" };

            _outputPortMock.Verify(presenter => presenter.AddNotifications(notificacoes), Times.Once());
        }

        [Fact]
        public async Task Deve_Adicionar_Notificacao_Quando_Fornecedor_Pessoa_Fisica_Sem_Data_Nascimento()
        {
            var sut = CriarSUT();
            var empresa = CriarEmpresa();

            var input = CriarCadastrarFornecedorInput(pessoaJuridica: false, cpfCnpj: "130.935.460-01", dataNascimento: null);
            _empresaRepositorioMock.Setup(x => x.ObterEmpresa(input.EmpresaId)).Returns(Task.FromResult(empresa));
            await sut.Execute(input);

            var notificacoes = new List<string>() { "Data de nascimento é obrigatório" };

            _outputPortMock.Verify(presenter => presenter.AddNotifications(notificacoes), Times.Once());
        }

        [Fact]
        public async Task Deve_Adicionar_Notificacao_Quando_Empresa_Do_Parana_Com_Fornecedor_Pessoa_Fisica_Menor_De_Idade()
        {
            var sut = CriarSUT();
            var empresa = CriarEmpresa();
            var input = CriarCadastrarFornecedorInput(pessoaJuridica: false, cpfCnpj: "130.935.460-01", dataNascimento: new DateTime(2005, 3, 11));

            _empresaRepositorioMock.Setup(x => x.ObterEmpresa(input.EmpresaId)).Returns(Task.FromResult(empresa));
            await sut.Execute(input);

            var notificacoes = new List<string>() { "Não é permitido cadastrar fornecedor pessoa física menor de idade" };

            _outputPortMock.Verify(presenter => presenter.AddNotifications(notificacoes), Times.Once());
        }

        [Fact]
        public async Task Deve_Adicionar_Notificacao_Quando_CNPJ_Invalido()
        {
            var sut = CriarSUT();
            var empresa = CriarEmpresa();
            var input = CriarCadastrarFornecedorInput(cpfCnpj: "1167289700011");
            _empresaRepositorioMock.Setup(x => x.ObterEmpresa(input.EmpresaId)).Returns(Task.FromResult(empresa));

            await sut.Execute(input);
            var notificacoes = new List<string>() { "CNPJ inválido" };
            _outputPortMock.Verify(presenter => presenter.AddNotifications(notificacoes), Times.Once());
        }

        [Fact]
        public async Task Deve_Adicionar_Notificacao_Quando_CPF_Invalido()
        {
            var sut = CriarSUT();
            var empresa = CriarEmpresa();
            var input = CriarCadastrarFornecedorInput(pessoaJuridica: false, cpfCnpj: "1167289700011", dataNascimento: new DateTime(1990, 3, 12));
            _empresaRepositorioMock.Setup(x => x.ObterEmpresa(input.EmpresaId)).Returns(Task.FromResult(empresa));
            await sut.Execute(input);

            var notificacoes = new List<string>() { "CPF inválido" };
            _outputPortMock.Verify(presenter => presenter.AddNotifications(notificacoes), Times.Once());
        }

        [Fact]
        public async Task Deve_Adicionar_Notificacao_Quando_CNPJ_Ja_Cadastrado()
        {
            var cnpj = new CNPJ("1167289700011");
            _fornecedorRepositorioMock.Setup(f => f.PessoaJuridicaCadastrada(cnpj)).Returns(Task.FromResult(true));

            var sut = CriarSUT();
            var input = CriarCadastrarFornecedorInput(cpfCnpj: "1167289700011");
            await sut.Execute(input);

            _outputPortMock.Verify(presenter => presenter.AddNotification($"CNPJ {cnpj} já cadastrado"), Times.Once());
        }

        [Fact]
        public async Task Deve_Adicionar_Notificacao_Quando_CPF_Ja_Cadastrado()
        {
            var cpf = new CPF("89071787044");
            _fornecedorRepositorioMock.Setup(f => f.PessoaFisicaCadastrada(cpf)).Returns(Task.FromResult(true));

            var sut = CriarSUT();
            var input = CriarCadastrarFornecedorInput(pessoaJuridica:false, cpfCnpj: "89071787044", dataNascimento: new DateTime(1990, 3, 12));
            await sut.Execute(input);

            _outputPortMock.Verify(presenter => presenter.AddNotification($"CPF {cpf} já cadastrado"), Times.Once());
        }

        [Fact]
        public async Task Deve_Adicionar_Notificacao_Quando_Empresa_Informada_Nao_Existir()
        {
            var sut = CriarSUT();
            var input = CriarCadastrarFornecedorInput(empresaId:Guid.NewGuid());

            _empresaRepositorioMock.Setup(e => e.ObterEmpresa(input.EmpresaId)).Returns(Task.FromResult(default(Empresa)));

            await sut.Execute(input);

            _outputPortMock.Verify(presenter => presenter.AddNotification($"Empresa não encontrada"), Times.Once());
        }

        [Fact]
        public async Task Deve_Adicionar_CadastrarFornecedorOutput_Corretamente_Quando_Fornecedor_Cadastrado()
        {
            var input = CriarCadastrarFornecedorInput();
            var cnpj = new CNPJ(input.CpfCnpj);
            var pessoaJuridica = _fornecedorFactory.NovaPessoaJuridica(input.Nome, input.CpfCnpj, null);
            var empresa = _empresaFactory.NovaEmpresa("PR", "Engie", input.CpfCnpj);
            var fornecedor = _fornecedorFactory.NovoFornecedor(empresa, pessoaJuridica);

            var outputMock = new CadastrarFornecedorOutput(fornecedor.Id, pessoaJuridica.Nome, pessoaJuridica.CNPJ.ToString(), pessoaJuridica.DataCadastro);
            var sut = CriarSUT(fornecedorFactoryMock: _fornecedorFactoryMock);
            
            _empresaRepositorioMock.Setup(x => x.ObterEmpresa(input.EmpresaId)).Returns(Task.FromResult(empresa));
            _fornecedorFactoryMock.Setup(f => f.NovaPessoaJuridica(input.Nome, input.CpfCnpj, new List<string>())).Returns(pessoaJuridica);
            _fornecedorFactoryMock.Setup(f => f.NovoFornecedor(empresa, pessoaJuridica)).Returns(fornecedor);
            _outputPortMock.Setup(p => p.Valid).Returns(true);

            CadastrarFornecedorOutput result = null;
            _outputPortMock.Setup(presenter => presenter.AddResult(It.IsAny<CadastrarFornecedorOutput>()))
           .Callback((CadastrarFornecedorOutput r) => result = r);

            await sut.Execute(input);

            Assert.Equal(outputMock.Id, result?.Id);
            Assert.Equal(outputMock.CpfCnpj, result?.CpfCnpj);
            Assert.Equal(outputMock.Nome, result?.Nome);
            Assert.Equal(outputMock.DataCadastro, result?.DataCadastro);
        }

        private CadastrarFornecedorCasoDeUso CriarSUT(Mock<IOutputPort> outputPortMock = null,
                                                      Mock<IFornecedorFactory> fornecedorFactoryMock = null,
                                                      Mock<IEmpresaRepositorio> empresaRepositorioMock = null,
                                                      Mock<IFornecedorRepositorio> fornecedorRepositorioMock = null,
                                                      Mock<IUnitOfWork> uowMock = null)
        {
            return new CadastrarFornecedorCasoDeUso(outputPortMock?.Object ?? _outputPortMock.Object,
                                                    fornecedorFactoryMock?.Object ?? _fornecedorFactory,
                                                    empresaRepositorioMock?.Object ?? _empresaRepositorioMock.Object,
                                                    fornecedorRepositorioMock?.Object ?? _fornecedorRepositorioMock.Object,
                                                    uowMock?.Object ?? _uowMock.Object);
        }

        private Empresa CriarEmpresa()
        {
            return _empresaFactory.NovaEmpresa("PR", "Engie", "89.330.056/0001-18");
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
