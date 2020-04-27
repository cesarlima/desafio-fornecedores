using System;
using System.Collections.Generic;
using System.Linq;
using Application.CasosDeUso.CadastrarEmpresa;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.CasosDeUso.CadastrarEmpresa
{
    public class CadastrarEmpresaPresenter : IOutputPort
    {
        private IActionResult _viewModel;
        private List<string> _notificacoes = new List<string>();
        public bool Valid { get { return !InValid; } }
        public bool InValid { get { return _notificacoes.Any(); } }

        public IActionResult ViewModel
        {
            get
            {
                if (InValid)
                {
                    _viewModel = new BadRequestObjectResult(_notificacoes);
                }

                return _viewModel ?? new NoContentResult();
            }
        }

        public void AddNotFoundMessage(string message)
        {
            _viewModel = new NotFoundObjectResult(message);
        }

        public void AddNotification(string message)
        {
            _notificacoes.Add(message);
        }

        public void AddResult(CadastrarEmpresaOutput output)
        {
            _viewModel = new ObjectResult(output);
        }
    }
}
