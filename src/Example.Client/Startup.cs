using Example.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Example.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Add auth services
            services.AddAuthorizationCore();
            services.AddScoped<AuthenticationStateProvider, Data.ClientAuthenticationStateProvider>();

            services.AddSingleton<IWeatherForecastService, Data.ClientWeatherForecastService>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
