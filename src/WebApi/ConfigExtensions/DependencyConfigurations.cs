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
        }
    }
}
