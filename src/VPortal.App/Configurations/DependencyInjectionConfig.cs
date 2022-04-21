using VPortal.App.Extensions;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using VPortal.Business.Interfaces;
using VPortal.Data.Context;
using VPortal.Data.Repository;

namespace VPortal.App.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<MeuDbContext>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IContaRepository, ContaRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddSingleton<IValidationAttributeAdapterProvider, MoedaValidationAttributeAdapterProvider>();


            return services;

        }
    }
}
