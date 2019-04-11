using System;
using System.Collections.Generic;
using System.Text;
using Mapster;

namespace SpeakFree.BLL.Mapster
{
	using SpeakFree.BLL.Dto.User;
	using SpeakFree.DAL.Models;

	/// <summary>
	/// Мапперр сущностей
	/// </summary>
	public static class MapperConfig
	{
		/// <summary>
		/// Конфигурировать сущности
		/// </summary>
		public static void Configure()
		{
			TypeAdapterConfig<User, UserDto>.NewConfig()
				.Map(dest => dest.Id, src => src.Id)
				.Map(dest => dest.Email, src => src.Email)
				.Map(dest => dest.Name, src => src.UserName)
				.Map(dest => dest.Patronymic, src => src.Patronymic)
				.Map(dest => dest.Sername, src => src.Surename);

		}
	}
}
