using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.CasosDeUso.ListarEmpresas;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
