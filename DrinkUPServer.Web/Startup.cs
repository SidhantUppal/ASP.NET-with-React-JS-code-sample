using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SpaServices;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DrinkUPServer.Web
{
    public class Startup
    {
        public Startup ( IConfiguration configuration )
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private IApplicationBuilder App;
        private IWebHostEnvironment Env;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices ( IServiceCollection services )
        {
            services.AddControllersWithViews();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles( configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            } );

            services.AddMvc().AddJsonOptions( SetJsonOptions );
        }

        private void SetJsonOptions ( JsonOptions jsonOptions )
        {
            jsonOptions.JsonSerializerOptions.IgnoreNullValues = true;
            jsonOptions.JsonSerializerOptions.MaxDepth = 64;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure ( IApplicationBuilder app, IWebHostEnvironment env )
        {
            App = app;
            Env = env;

            if ( Env.IsDevelopment() )
            {
                App.UseDeveloperExceptionPage();
            }
            else
            {
                App.UseExceptionHandler( "/Error" );
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                App.UseHsts();
            }

            App.UseHttpsRedirection();
            App.UseStaticFiles();
            App.UseSpaStaticFiles();

            App.UseRouting();

            App.UseEndpoints( ConstructEndpoints );
            App.UseSpa( ConstructSpa );
        }

        private void ConstructEndpoints ( IEndpointRouteBuilder endpoint )
        {
            endpoint.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}"
                    );
        }

        private void ConstructSpa ( ISpaBuilder spa )
        {
            spa.Options.SourcePath = "ClientApp";

            if ( Env.IsDevelopment() )
            {
                spa.UseReactDevelopmentServer( npmScript: "start" );
            }
        }
    }
}
