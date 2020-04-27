using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Application.CasosDeUso.CadastrarEmpresa;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.CasosDeUso.CadastrarEmpresa
{
    [ApiController]
    [Route("api/[controller]")]
    public class CadastrarEmpresaController : ControllerBase
    {
        // GET: /<controller>/
        public string Index()
        {
            return "Funciona";
        }


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
