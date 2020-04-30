using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Fornecedores;
using Domain.Fornecedores.ValueObject;

namespace Application.CasosDeUso.ListarFornecedores
{
    public class ListarFornecedoresCasoDeUso : IUseCase<ListarFornecedoresInput>
    {
        private readonly IOutputPort _outputPort;
        private readonly IFornecedorRepositorio _fornecedorRepositorio;

        public ListarFornecedoresCasoDeUso(IOutputPort outputPort, IFornecedorRepositorio fornecedorRepositorio)
        {
            _outputPort = outputPort ?? throw new ArgumentNullException(nameof(outputPort));
            _fornecedorRepositorio = fornecedorRepositorio ?? throw new ArgumentNullException(nameof(fornecedorRepositorio));
        }

        public async Task Execute(ListarFornecedoresInput input)
        {
            var fornecedores = await _fornecedorRepositorio.ObterFornecedores(input.Nome, input.CpfCnpj, input.DataCadastro);

            var fornecedorOutput = new List<Fornecedor>();
            foreach (var forn in fornecedores)
            {
                fornecedorOutput.Add(new Fornecedor(forn.Id,
                                                    forn.Pessoa.Nome,
                                                    forn.Pessoa.ObterNumeroCpfCnpj(),
                                                    forn.Pessoa.PessoaTipo == PessoaTipo.PessoaJuridica,
                                                    forn?.Pessoa?.Telefones.Select(x => x.Numero)));
            }

            _outputPort.AddResult(new ListarFornecedoresOutput(fornecedorOutput));
        }
    }
}
