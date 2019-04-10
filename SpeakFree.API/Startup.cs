using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SpeakFree.API
{
	using SpeakFree.BLL;
	using SpeakFree.DAL;
	using Swashbuckle.AspNetCore.Swagger;
    using System;
    using System.IO;
	using System.Linq;
	using System.Reflection;

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
			services.AddSwaggerGen(
				c =>
					{
						c.SwaggerDoc("v1",
							new Info
							{
								Version = "v1",
								Title = "Speak Free API",
							});

						// Set the comments path for the Swagger JSON and UI.
						var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
						var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
						c.IncludeXmlComments(xmlPath);
					});

			services.AddDataAccessCollection(Configuration);
			services.AddLogicServicesCollection();

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }


            app.UseCors(); // Изучить !!!!!!

            app.UseSwagger();
            app.UseSwaggerUI(
	            c =>
		            {
			            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Speak Free API");
		            });

			app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseMvc();

           
        }
    }
}
