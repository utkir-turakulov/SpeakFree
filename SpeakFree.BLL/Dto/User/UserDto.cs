using System;
using System.Collections.Generic;
using System.Text;

namespace SpeakFree.BLL.Dto.User
{
	/// <summary>
	/// Модель данных пользователя
	/// </summary>
	public class UserDto
	{
		/// <summary>
		/// Id пользователя
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// Почта(логин) потлтзователя
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Имя
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Фамилия
		/// </summary>
		public string Sername { get; set; }

		/// <summary>
		/// Отчество
		/// </summary>
		public string Patronymic { get; set; }
	}
}
