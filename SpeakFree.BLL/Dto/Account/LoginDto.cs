namespace SpeakFree.BLL.Dto.Account
{
	public class LoginDto
    {
		/// <summary>
		/// Email
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Пароль
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		/// Запомнить меня
		/// </summary>
		public bool RememberMe { get; set; }
    }
}
