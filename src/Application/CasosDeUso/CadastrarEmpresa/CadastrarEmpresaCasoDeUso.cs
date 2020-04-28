using System;
using System.Threading.Tasks;
using Application.Services;
using Domain.Empresas;

namespace Application.CasosDeUso.CadastrarEmpresa
{
    public class CadastrarEmpresaCasoDeUso : IUseCase<CadastrarEmpresaInput>
    {
        private readonly IOutputPort _outputPort;
        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly IEmpresaFactory _empresaFactory;
        private readonly IUnitOfWork _unitOfWork;

        public CadastrarEmpresaCasoDeUso(IOutputPort outputPort,
                                         IEmpresaRepositorio empresaRepositorio,
                                         IEmpresaFactory empresaFactory,
                                         IUnitOfWork unitOfWork)
        {
            _outputPort = outputPort ?? throw new ArgumentNullException(nameof(outputPort));
            _empresaRepositorio = empresaRepositorio ?? throw new ArgumentNullException(nameof(empresaRepositorio));
            _empresaFactory = empresaFactory ?? throw new ArgumentNullException(nameof(empresaFactory));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task Execute(CadastrarEmpresaInput input)
        {
            var empresa = _empresaFactory.NovaEmpresa(input.UF, input.NomeFantasia, input.CNPJ);

            if (empresa.Invalido)
            {
                _outputPort.AddNotifications(empresa.Notificacoes);
                return;
            }

            if (await _empresaRepositorio.EmpresaJaCadastrada(empresa.CNPJ))
            {
                _outputPort.AddNotification("CNPJ já cadastrado");
                return;
            }
                
            await _empresaRepositorio.Save(empresa).ConfigureAwait(false);

            await _unitOfWork.Commit();

            _outputPort.AddResult(new CadastrarEmpresaOutput(empresa.Id, empresa.UF, empresa.NomeFantasia, empresa.CNPJ.ToString()));
        }
    }
}