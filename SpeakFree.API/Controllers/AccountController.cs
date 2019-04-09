﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpeakFree.BLL.Dto.Account;
using SpeakFree.DAL.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SpeakFree.API.Controllers
{
	/// <summary>
	/// Аккаунт контроллер
	/// </summary>
	[Produces("application/json")]  
	[Route("api/[controller]")]
	public class AccountController : Controller
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;

		public AccountController(UserManager<User> userManager,SignInManager<User> signInManager)
		{
			this._userManager = userManager;
			this._signInManager = signInManager;
		}

		/// <summary>
		/// Авторизация
		/// </summary>
		/// <param name="model"></param>
		// POST api/<controller>
		[HttpPost("Login")]
		public async Task<IActionResult> Login([FromBody]LoginDto model)
		{
			if (ModelState.IsValid)
			{
				var result = await this._signInManager.PasswordSignInAsync(model.Email,model.Password,model.RememberMe,false);

				if (result.Succeeded)
				{
					return Json(new
					{
						Result = "Loged In"
					}
					);
				}
			}

			return  Json(new {Result = "Failed"});
		}

		/// <summary>
		/// Разлогиниться
		/// </summary>
		/// <returns></returns>
		[HttpPost("Logout")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> LogOut()
		{
			// удаляем аутентификационные куки
			await _signInManager.SignOutAsync();
			return RedirectToAction("Login", "Account");
		}

		/// <summary>
		/// Изменить пользователя
		/// </summary>
		/// <param name="id"></param>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost("Edit/{id}")]
		public async Task<IActionResult> EditUser(long id, [FromBody]RegistrationDto model)
		{
			return  Json(new { Result = "Not Implemented" });
		}

		/// <summary>
		/// Регистрация
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost("Register")]
		public async Task<IActionResult> Register([FromBody] RegistrationDto model)
		{
			if (ModelState.IsValid)
			{
				User user = new User()
					            {
									Email = model.Email,
									UserName = model.Name,
									Surename = model.Surename,
									Patronymic = model.Patronymic,
					            };

				var result = await this._userManager.CreateAsync(user, model.Password);

				if (result.Succeeded)
				{
					return Json(new { Result = "Registered" });
				}
				else
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
				}
			}

			return Json(new { Result = "Failed" });
		}
	}
}
