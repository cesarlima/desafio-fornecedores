using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Empresas;

namespace Application.CasosDeUso.ListarEmpresasCasoDeUso
{
    public class ListarEmpresasCasoDeUso : IUseCase<ListarEmpresaInput>
    {
        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly IListarEmpresasPresenter _presenter;

        public ListarEmpresasCasoDeUso(IEmpresaRepositorio empresaRepositorio, IListarEmpresasPresenter presenter)
        {
            _empresaRepositorio = empresaRepositorio ?? throw new ArgumentNullException(nameof(empresaRepositorio));
            _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
        }

        public async Task Execute(ListarEmpresaInput input)
        {
            var empresas = await _empresaRepositorio.ObterEmpresas();
            var empresasOutput = new List<Empresa>();

            foreach (var emp in empresas)
            {
                empresasOutput.Add(new Empresa(emp.Id, emp.CNPJ.ToString()));
            }

            _presenter.AddResult(new ListarEmpresaOutput(empresasOutput));
        }
    }
}
