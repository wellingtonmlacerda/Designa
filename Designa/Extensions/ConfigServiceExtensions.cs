using Designa.Models;

namespace Designa.Extensions
{
    public static class ConfigServiceExtensions
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            services.AddScoped<IPublicacao, Publicacao>();
            services.AddScoped<IReuniaoFactory, ReuniaoFactory>();

            return services;
        }
    }
}
