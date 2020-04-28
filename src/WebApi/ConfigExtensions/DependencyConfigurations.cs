﻿using System;
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

            services.AddScoped<CasosDeUso.CadastrarFornecedor.CadastrarFornecedorPresenter>();
            services.AddScoped<Application.CasosDeUso.CadastrarFornecedor.IOutputPort>(x => x.GetRequiredService<CasosDeUso.CadastrarFornecedor.CadastrarFornecedorPresenter>());

            services.AddScoped<Domain.Empresas.IEmpresaRepositorio, Infra.Repositories.EmpresaRepositorio>();
            services.AddScoped<Domain.Fornecedores.IFornecedorRepositorio, Infra.Repositories.FornecedorRepositorio>();
            services.AddScoped<Domain.Empresas.IEmpresaFactory, Infra.Entities.EntityFactories>();
            services.AddScoped<Domain.Fornecedores.IFornecedorFactory, Infra.Entities.EntityFactories>();


            services.AddScoped<Application.CasosDeUso.ListarEmpresas.ListarEmpresasCasoDeUso>();
            services.AddScoped<Application.CasosDeUso.CadastrarEmpresa.CadastrarEmpresaCasoDeUso>();
            services.AddScoped<Application.CasosDeUso.CadastrarFornecedor.CadastrarFornecedorCasoDeUso>();
            services.AddScoped<Application.Services.IUnitOfWork, Infra.Transaction.UnitOfWork>();
        }
    }
}
