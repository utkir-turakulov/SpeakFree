using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace SpeakFree.API.AuthOptions
{
	/// <summary>
	/// Опции авторизации по токенам
	/// </summary>
	public class AuthOptions
	{
		/// <summary>
		/// Издатель токена
		/// </summary>
		public const string ISSUER = "authentication_server";

		/// <summary>
		/// Потребитель токена
		/// </summary>
		public const string AUDIENCE = "speak_free_user";

		/// <summary>
		/// Ключ шифрации токена
		/// </summary>
		const string KEY = "secret_key_speak_free_api";

		/// <summary>
		/// Время жизни токена по умолчанию 1 день
		/// </summary>
		public TimeSpan LIFETIME { get;  set; } = new TimeSpan(1,0,0); 

		/// <summary>
		/// Получить код безопасности
		/// </summary>
		/// <returns></returns>
		public static SymmetricSecurityKey GetSymmetricSecurityKey()
		{
			return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
		}
	}
}
