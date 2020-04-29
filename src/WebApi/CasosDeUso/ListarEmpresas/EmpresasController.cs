using System.Threading.Tasks;
using Application.CasosDeUso.ListarEmpresas;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.CasosDeUso.ListarEmpresas
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresasController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromServices]ListarEmpresasPresenter presenter,
            [FromServices]ListarEmpresasCasoDeUso casoDeUso)
        {
            await casoDeUso.Execute(new ListarEmpresaInput());
            return presenter.ViewModel;
        }
    }
}
