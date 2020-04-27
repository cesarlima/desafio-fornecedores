using System;
using System.Threading.Tasks;
using Application.Services;
using Domain.Common.ValueObjects;
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
            if (ValidarSeInformadoRgEDataNascimentoPessoaFisica(input) == false)
                return;

            if (ValidarSeFornecedorPessoaFisicaDoParanaMaiorDeIdade(input) == false)
                return;

            Pessoa pessoa;
            if (input.PessoaJuridica)
                pessoa = CriarPessoaJuridica(input);
             else
                pessoa = CriarPessoaFisica(input);

            if (_outputPort.Valid)
            {
                var empresa = await _empresaRepositorio.ObterEmpresa(input.EmpresaId);
                var fornecedor = _fornecedorFactory.NovoFornecedor(empresa, pessoa);
                await _fornecedorRepositorio.Salvar(fornecedor);
                await _unitOfWork.Commit();

                var result = new CadastrarFornecedorOutput(fornecedor.Id, pessoa.Nome, pessoa.ObterNumeroCpfCnpj(), pessoa.DataCadastro);
                _outputPort.AddResult(result);
            }
        }

        private PessoaFisica CriarPessoaFisica(CadastrarFornecedorInput input)
        {
            var cpf = new CPF(input.CpfCnpj);

            if (cpf.Valido == false)
                _outputPort.AddNotification("CPF inválido");

            return _fornecedorFactory.NovaPessoaFisica(input.Nome, input.RG, input.DataNascimento.Value, cpf);
        }

        private PessoaJuridica CriarPessoaJuridica(CadastrarFornecedorInput input)
        {
            var cnpj = new CNPJ(input.CpfCnpj);

            if (cnpj.Valido == false)
                _outputPort.AddNotification("CNPJ inválido");

            return _fornecedorFactory.NovaPessoaJuridica(input.Nome, cnpj);
        }

        private bool ValidarSeInformadoRgEDataNascimentoPessoaFisica(CadastrarFornecedorInput input)
        {
            var valido = true;

            if (input.PessoaJuridica == false)
            {
                if (string.IsNullOrEmpty(input.RG))
                {
                    _outputPort.AddNotification("Fornecedor pessoa física deve informar o RG");
                    valido = false;
                }
                    
                if (input.DataNascimento == null)
                {
                    _outputPort.AddNotification("Fornecedor pessoa física deve informar a data de nascimento");
                    valido = false;
                }
            }

            return valido;
        }

        private bool ValidarSeFornecedorPessoaFisicaDoParanaMaiorDeIdade(CadastrarFornecedorInput input)
        {
            var valido = true;

            if (input.UF == "PR" && input.PessoaJuridica == false)
            {
                var menorDeIdade = (DateTime.Now.Subtract(input.DataNascimento.Value).TotalDays / 365.2425) < 18;

                if (menorDeIdade)
                {
                    _outputPort.AddNotification("Não é permitido cadastrar fornecedor pessoa física menor de idade");
                    valido = false;
                }    
            }

            return valido;
        }   
    }
}
