using System;
using System.Threading.Tasks;
using Domain.Common.ValueObjects;
using Domain.Fornecedores;

namespace Application.CasosDeUso.CadastrarFornecedor
{
    public class CadastrarFornecedorCasoDeUso : IUseCase<CadastrarFornecedorInput>
    {
        private readonly IOutputPort _outputPort;
        private readonly IPessoaFactory _pessoaFactory;


        public CadastrarFornecedorCasoDeUso(IOutputPort outputPort, IPessoaFactory pessoaFactory)
        {
            _outputPort = outputPort ?? throw new ArgumentNullException(nameof(outputPort));
            _pessoaFactory = pessoaFactory ?? throw new ArgumentNullException(nameof(pessoaFactory));
        }

        public async Task Execute(CadastrarFornecedorInput input)
        {
            if (ValidarSeInformadoRgEDataNascimentoPessoaFisica(input) == false)
                return;

            if (ValidarSeFornecedorPessoaFisicaDoParanaMaiorDeIdade(input) == false)
                return;

            Pessoa pessoa = null;

            if (input.PessoaJuridica)
                pessoa = CriarPessoaJuridica(input);
             else
                pessoa = CriarPessoaFisica(input);



        }

        private PessoaFisica CriarPessoaFisica(CadastrarFornecedorInput input)
        {
            var cpf = new CPF(input.CpfCnpj);

            if (cpf.Valido == false)
                _outputPort.AddNotification("CPF inválido");

            return _pessoaFactory.NovaPessoaFisica(input.Nome, input.RG, input.DataNascimento.Value, cpf);
        }

        private PessoaJuridica CriarPessoaJuridica(CadastrarFornecedorInput input)
        {
            var cnpj = new CNPJ(input.CpfCnpj);

            if (cnpj.Valido == false)
                _outputPort.AddNotification("CNPJ inválido");

            return _pessoaFactory.NovaPessoaJuridica(input.Nome, cnpj);
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
