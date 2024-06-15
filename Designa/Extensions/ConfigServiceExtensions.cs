using Designa.Models;

namespace Designa.Extensions
{
    public static class ConfigServiceExtensions
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            services.AddScoped<IPublicacao, Publicacao>();
            services.AddScoped<IReuniaoFactory, ReuniaoFactory>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddControllers()
                        .AddJsonOptions(options =>
                        {
                            options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                        });

            return services;
        }
    }
}
