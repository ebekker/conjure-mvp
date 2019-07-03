using Example.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using System.Linq;

namespace Example.Server
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddNewtonsoftJson();

            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            if (ServerGlobals.IsBlazorServerSide)
            {
                services.AddServerSideBlazor();
            }

            services.AddSingleton<IWeatherForecastService, Data.ServerWeatherForecastService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBlazorDebugging();
            }
            else
            {
                app.UseHttpsRedirection();
            }

            app.UseResponseCompression();
            app.UseClientSideBlazorFiles<Client.Startup>();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                if (ServerGlobals.IsBlazorServerSide)
                {
                    endpoints.MapBlazorHub();
                }
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
