using System;
using System.Threading.Tasks;
using Domain.Fornecedores;

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
        }
    }
}
