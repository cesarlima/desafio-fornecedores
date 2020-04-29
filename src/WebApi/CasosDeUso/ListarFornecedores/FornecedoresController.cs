using System.Threading.Tasks;
using Application.CasosDeUso.ListarFornecedores;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.CasosDeUso.ListarFornecedores
{
    [ApiController]
    [Route("api/[controller]")]
    public class Fornecedores : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromServices]ListarFornecedoresPresenter presenter,
             [FromServices]ListarFornecedoresCasoDeUso casoDeUso, [FromQuery]ListarFornecedoresRequest request)
        {
            await casoDeUso.Execute(new ListarFornecedoresInput(request.Nome, request.CpfCnpj, request.DataCadastro));
            return presenter.ViewModel;
        }
    }
}
