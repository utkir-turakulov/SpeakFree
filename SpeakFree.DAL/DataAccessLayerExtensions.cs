using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;

namespace SpeakFree.DAL
{
	using Microsoft.AspNetCore.Identity.UI;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	using SpeakFree.DAL.Context;
	using SpeakFree.DAL.Models;
	using SpeakFree.DAL.Services;
	using SpeakFree.DAL.Services.Implementation;

	/// <summary>
	/// Регистрирует зависимости DAL 
	/// </summary>
	public static class DataAccessLayerExtensions
    {
	    /// <summary>
	    /// Добаляет сервисы DAL слоя
	    /// </summary>
	    /// <param name="services">сервисы</param>
	    /// <param name="configuration">конфигурация</param>
	    /// <returns>коллекция сервисов</returns>
		public static IServiceCollection AddDataAccessCollection(
		    this IServiceCollection services,
		    IConfiguration configuration)
	    {
		    services.AddScoped<IMessageDbService, MessageDbService>();
		    services.AddScoped<IUserDbService, UserDbService>();

		    services.AddDbContext<SpeakFreeDataContext>(
			    option => option.UseSqlServer(configuration.GetConnectionString("SpeakFreeConnection")));

		    services.AddIdentity<User, IdentityRole>(
			    options =>
				    {
					    options.Password.RequiredLength = 6; // минимальная длина
					    options.Password.RequireNonAlphanumeric = false; // требуются ли не алфавитно-цифровые символы
					    options.Password.RequireLowercase = false; // требуются ли символы в нижнем регистре
					    options.Password.RequireUppercase = false; // требуются ли символы в верхнем регистре
					    options.Password.RequireDigit = false; // требуются ли цифры
				    })
			    .AddRoleManager<RoleManager<IdentityRole>>()
			    .AddDefaultUI(UIFramework.Bootstrap4)
			    .AddRoles<IdentityRole>()
			    .AddEntityFrameworkStores<SpeakFreeDataContext>();

			return services;
	    }
    }
}
