using System.Threading.Tasks;
using Application.CasosDeUso.CadastrarEmpresa;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.CasosDeUso.CadastrarEmpresa
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresasController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromServices]CadastrarEmpresaPresenter presenter,
            [FromServices]CadastrarEmpresaCasoDeUso casoDeUso,
            [FromBody]CadastrarEmpresaRequest request)
        {
            var input = new CadastrarEmpresaInput(request.UF, request.NomeFantasia, request.CNPJ);
            await casoDeUso.Execute(input);

            return presenter.ViewModel;
        }
    }
}
