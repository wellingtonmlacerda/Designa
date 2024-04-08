using Designa.DAL;
using Designa.Data;
using Designa.Models;
using Microsoft.EntityFrameworkCore;

namespace Designa.Extensions
{
    public static class ConfigServiceExtensions
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            services.AddScoped<IPublicacao, Publicacao>();
            services.AddScoped<IReuniaoFactory, ReuniaoFactory>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}
