using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.ConfigExtensions
{
    public static class DependencyConfigurations
    {
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<Infra.Contexts.FornecedoresContext>(
                   options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


          
            services.AddScoped<CasosDeUso.ListarEmpresas.ListarEmpresasPresenter>();
            services.AddScoped<Application.CasosDeUso.ListarEmpresas.IListarEmpresasPresenter>(x => x.GetRequiredService<CasosDeUso.ListarEmpresas.ListarEmpresasPresenter>());

            services.AddScoped<CasosDeUso.CadastrarEmpresa.CadastrarEmpresaPresenter>();
            services.AddScoped<Application.CasosDeUso.CadastrarEmpresa.IOutputPort>(x => x.GetRequiredService<CasosDeUso.CadastrarEmpresa.CadastrarEmpresaPresenter>());

            services.AddScoped<Domain.Empresas.IEmpresaRepositorio, Infra.Repositories.EmpresaRepositorio>();
            services.AddScoped<Domain.Empresas.IEmpresaFactory, Infra.Entities.EntityFactories>();
            

            services.AddScoped<Application.CasosDeUso.ListarEmpresas.ListarEmpresasCasoDeUso>();
            services.AddScoped<Application.CasosDeUso.CadastrarEmpresa.CadastrarEmpresaCasoDeUso>();
            services.AddScoped<Application.Services.IUnitOfWork, Infra.Transaction.UnitOfWork>();
        }
    }
}
