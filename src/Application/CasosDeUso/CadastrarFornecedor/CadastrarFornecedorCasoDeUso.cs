using System;
using System.Threading.Tasks;
using Application.Services;
using Domain.Empresas;
using Domain.Fornecedores;

namespace Application.CasosDeUso.CadastrarFornecedor
{
    public class CadastrarFornecedorCasoDeUso : IUseCase<CadastrarFornecedorInput>
    {
        private readonly IOutputPort _outputPort;
        private readonly IFornecedorFactory _fornecedorFactory;
        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly IFornecedorRepositorio _fornecedorRepositorio;
        private readonly IUnitOfWork _unitOfWork;

        public CadastrarFornecedorCasoDeUso(IOutputPort outputPort,
            IFornecedorFactory fornecedorFactory,
            IEmpresaRepositorio empresaRepositorio,
            IFornecedorRepositorio fornecedorRepositorio,
            IUnitOfWork unitOfWork)
        {
            _outputPort = outputPort ?? throw new ArgumentNullException(nameof(outputPort));
            _fornecedorFactory = fornecedorFactory ?? throw new ArgumentNullException(nameof(fornecedorFactory));
            _empresaRepositorio = empresaRepositorio ?? throw new ArgumentNullException(nameof(empresaRepositorio));
            _fornecedorRepositorio = fornecedorRepositorio ?? throw new ArgumentNullException(nameof(fornecedorRepositorio));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task Execute(CadastrarFornecedorInput input)
        {
            Pessoa pessoa;
            if (input.PessoaJuridica)
            {
                var pessoaJuridica = _fornecedorFactory.NovaPessoaJuridica(input.Nome, input.CpfCnpj, input.Telefones);

                if (await _fornecedorRepositorio.PessoaJuridicaCadastrada(pessoaJuridica.CNPJ))
                {
                    _outputPort.AddNotification($"CNPJ {pessoaJuridica.CNPJ} já cadastrado");
                    return;
                }

                pessoa = pessoaJuridica;
            }
            else
            {
                var pessoaFisica = _fornecedorFactory.NovaPessoaFisica(input.Nome, input.RG, input.DataNascimento, input.CpfCnpj, input.Telefones);

                if (await _fornecedorRepositorio.PessoaFisicaCadastrada(pessoaFisica.CPF))
                {
                    _outputPort.AddNotification($"CPF {pessoaFisica.CPF} já cadastrado");
                    return;
                }

                pessoa = pessoaFisica;
            }
                
            var empresa = await _empresaRepositorio.ObterEmpresa(input.EmpresaId);

            if (empresa == null)
            {
                _outputPort.AddNotification("Empresa não encontrada");
                return;
            }

            var fornecedor = _fornecedorFactory.NovoFornecedor(empresa, pessoa);

            _outputPort.AddNotifications(fornecedor.Notificacoes);

            if (_outputPort.Valid)
            {
                await _fornecedorRepositorio.Salvar(fornecedor);
                await _unitOfWork.Commit();

                var result = new CadastrarFornecedorOutput(fornecedor.Id, pessoa.Nome, pessoa.ObterNumeroCpfCnpj(), pessoa.DataCadastro);
                _outputPort.AddResult(result);
            }
        }
    }
}
