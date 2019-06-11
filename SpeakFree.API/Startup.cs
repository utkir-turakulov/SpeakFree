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
	using System.Reflection;
	using Microsoft.AspNetCore.Authentication.JwtBearer;
	using Microsoft.IdentityModel.Tokens;
	using SpeakFree.API.Services;
	using AuthOption = AuthOptions.AuthOptions;
	using SpeakFree.API.Controllers;

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
			
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
				options =>
					{
						// использование ssl при отправке токена
						options.RequireHttpsMetadata = true;
						options.TokenValidationParameters = new TokenValidationParameters
						{
							// укзывает, будет ли валидироваться издатель при валидации токена
							ValidateIssuer = true,
							// строка, представляющая издателя
							ValidIssuer = AuthOption.ISSUER,
							// будет ли валидироваться потребитель токена
							ValidateAudience = true,
							// установка потребителя токена
							ValidAudience = AuthOption.AUDIENCE,
							// будет ли валидироваться время существования
							ValidateLifetime = true,
 
							// установка ключа безопасности
							IssuerSigningKey = AuthOption.GetSymmetricSecurityKey(),
							// валидация ключа безопасности
							ValidateIssuerSigningKey = true,

						};
					});
			services.AddScoped<TokenService>();
			services.AddDataAccessCollection(Configuration);
			services.AddLogicServicesCollection();
			services.AddSignalR();
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
				app.UseExceptionHandler("/Home/Error");
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
			app.UseStaticFiles();
			app.UseCookiePolicy();

			app.UseSignalR(routes =>
			{
				routes.MapHub<ChatHub>("/chatter");
			});

			app.UseMvc(routes =>
				{
					routes.MapRoute(
					 name: "default",
					 template: "{controller=Home}/{action=Index}/{id?}");
				});
		}
    }
}
