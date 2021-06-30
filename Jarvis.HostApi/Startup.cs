using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Jarvis.HostApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
                config.ApiVersionReader = new QueryStringApiVersionReader("api-version");
            });

            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc(
            //        "1.0",
            //        new OpenApiInfo
            //        {
            //            Title = "Jarvis.Host.Api",
            //            Version = "1.0"
            //        });

            //    c.SwaggerDoc(
            //        "2.0",
            //        new OpenApiInfo
            //        {
            //            Title = "Jarvis.Host.Api",
            //            Version = "2.0"
            //        });
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                //app.UseSwaggerUI(
                //    c =>
                //    {
                //        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Jarvis.Host.Api v1");
                //        c.SwaggerEndpoint("/swagger/v2/swagger.json", "Jarvis.Host.Api v2");
                //    });

                app.UseSwaggerUI(
                   options =>
                   {
                       foreach (var description in provider.ApiVersionDescriptions)
                       {
                           options.SwaggerEndpoint(
                               $"/swagger/{description.GroupName}/swagger.json",
                               description.GroupName.ToUpperInvariant());
                       }
                   });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
        {
            readonly IApiVersionDescriptionProvider provider;

            public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) =>
              this.provider = provider;

            public void Configure(SwaggerGenOptions options)
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(
                      description.GroupName,
                        new OpenApiInfo()
                        {
                            Title = $"Sample API {description.ApiVersion}",
                            Version = description.ApiVersion.ToString(),
                        });
                }
            }
        }
    }
}
