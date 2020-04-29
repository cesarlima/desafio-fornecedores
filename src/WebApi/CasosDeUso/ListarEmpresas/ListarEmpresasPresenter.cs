using System.Collections.Generic;
using System.Linq;
using Application.CasosDeUso.ListarEmpresas;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.CasosDeUso.ListarEmpresas
{
    public class ListarEmpresasPresenter : IOutputPort
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

        public void AddNotification(string message)
        {
            _notificacoes.Add(message);
        }

        public void AddNotifications(IEnumerable<string> messages)
        {
            _notificacoes.AddRange(messages);
        }

        public void AddResult(ListarEmpresaOutput output)
        {
            _viewModel = new ObjectResult(output);
        }
    }
}
