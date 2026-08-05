using Microsoft.Extensions.DependencyInjection;

namespace OasisApi.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<Services.IMoradorService, Services.MoradorService>();
            services.AddScoped<Services.IAlojamentoService, Services.AlojamentoService>();
            services.AddScoped<Services.IAuthService, Services.AuthService>();

            return services;
        }
    }
}
