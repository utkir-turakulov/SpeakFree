using System;
using System.Collections.Generic;
using System.Text;

namespace SpeakFree.BLL
{
	using Microsoft.Extensions.DependencyInjection;

	using SpeakFree.BLL.Services;
	using SpeakFree.BLL.Services.Implementation;

	/// <summary>
	/// Регистрирует зависимости BLL
	/// </summary>
	public static class BusinessLogicExtensions
    {
		/// <summary>
		/// Добавляет сервисы BLL слоя 
		/// </summary>
		/// <param name="services">сервисы</param>
		/// <returns>коллекция сервисов</returns>
	    public static IServiceCollection AddLogicServicesCollection(this IServiceCollection services)
	    {
		    services.AddScoped<IMessageService, MessageOperationService>();

		    return services;
	    }
    }
}
