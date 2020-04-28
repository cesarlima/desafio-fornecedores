using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.CasosDeUso.CadastrarFornecedor;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.CasosDeUso.CadastrarFornecedor
{
    [ApiController]
    [Route("api/[controller]")]
    public class Fornecedores : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromServices]CadastrarFornecedorPresenter presenter,
            [FromServices]CadastrarFornecedorCasoDeUso casoDeUso,
            [FromBody]CadastrarFornecedorRequest request)
        {
            var input = new CadastrarFornecedorInput(request.EmpresaId,
                                                     request.Nome,
                                                     request.CpfCnpj,
                                                     request.UF,
                                                     request.RG,
                                                     request.Telefones,
                                                     request.PessoaJuridica,
                                                     request.DataNascimento);

            await casoDeUso.Execute(input);

            return presenter.ViewModel;
        }
    }
}
