using System;
using System.Collections.Generic;
using System.Text;

namespace SpeakFree.BLL.Dto.Account
{
	/// <summary>
	/// Модель регистрации пользователя 
	/// </summary>
    public class RegistrationDto 
    {
		/// <summary>
		/// Email
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Имя
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Отчество
		/// </summary>
		public string Patronymic { get; set; }

		/// <summary>
		/// Фамилия
		/// </summary>
		public string Surename { get; set; }

		/// <summary>
		/// Пароль
		/// </summary>
		public string Password { get; set; }

    }
}
