using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpeakFree.BLL.Dto.Account;
using SpeakFree.DAL.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SpeakFree.API.Controllers
{
	using System;
	using System.IdentityModel.Tokens.Jwt;
	using System.Linq;
	using AuthOptions;

	using Microsoft.AspNetCore.Authorization;
	using Microsoft.IdentityModel.Tokens;

	using SpeakFree.API.Services;
    using SpeakFree.BLL.Services;
    using SpeakFree.BLL.Services.Implementation;

    /// <summary>
    /// Аккаунт контроллер
    /// </summary>
   // [Produces("application/json")]  
	[Route("/[controller]")]
	public class AccountController : Controller
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;

		private readonly RoleManager<IdentityRole> _roleManager;

		private readonly TokenService _tokenService;

        private readonly IUserOperationService _userOperationService;

		public AccountController(
			UserManager<User> userManager,
		    SignInManager<User> signInManager, 
			RoleManager<IdentityRole> roleManager,
			TokenService tokenService,
            IUserOperationService userOperationService)
		{
			this._userManager = userManager;
			this._signInManager = signInManager;
			this._roleManager = roleManager;
			this._tokenService = tokenService;
            this._userOperationService = userOperationService;
		}

		/// <summary>
		/// Авторизация
		/// </summary>
		/// <param name="model"></param>
		// POST api/<controller>
		[AllowAnonymous]
		[HttpPost("Login")]
		public async Task<IActionResult> Login(LoginDto model)
		{
			if (ModelState.IsValid)
			{
				var result = await this._signInManager.PasswordSignInAsync(model.Email,model.Password,model.RememberMe,false);
                var user = await (_userOperationService.GetByLogin(model.Email));

                if (user != null)
                {
                    if (result.Succeeded)
                    {
                        if (string.IsNullOrWhiteSpace(model.ReturnUrl))
                        {
                            return RedirectToActionPermanent("Index", "Home");
                        }
                        else
                        {
                            return RedirectPermanent(model.ReturnUrl);                            
                        }
                    }
                    else
                    {
                        if (result.IsLockedOut)
                        {
                            ModelState.AddModelError("", "Пользователь заблокирован");
                        }
                        else if (result.IsNotAllowed)
                        {
                            ModelState.AddModelError("", "Вход пользователю запрещён");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Неверный пароль пользователя");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неверное имя пользователя");
                }
			}
			else
			{
				return View("Register");
			}

			return View(model);
        }

		[AllowAnonymous]
		[HttpGet("Login")]
		public async Task<IActionResult> Login(string returnUrl)
		{
            LoginDto model = new LoginDto()
            {
                ReturnUrl = returnUrl
            };

            return View(model);
		}

		/// <summary>
		/// Разлогиниться
		/// </summary>
		/// <returns></returns>
		[HttpGet("Logout")]
	//	[ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout()
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
		public async Task<IActionResult> Register( RegistrationDto model)
		{
			if (ModelState.IsValid)
			{
				User user = new User()
					            {
									Name = model.Name,
									Email = model.Email,
									UserName = model.Email,
									Surename = model.Surename,
									Patronymic = model.Patronymic,
					            };

				var result = await this._userManager.CreateAsync(user, model.Password);

				if (result.Succeeded)
				{
					if (string.IsNullOrEmpty(model.ReturnUrl))
					{
						return RedirectToAction("GetTasks", "TaskBoard");
					}
					return RedirectToAction(model.ReturnUrl.Split("/")[1], model.ReturnUrl.Split("/")[0]);
				}
				else
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
				}
			}

			return View("Register");
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		[HttpGet("Register")]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost("Token")]
		public async Task<IActionResult> Token([FromBody]LoginDto model)
		{
			var identity = await this._tokenService.GetIdentity(model.Email, model.Password);

			if (identity == null)
			{
				return this.NotFound("Invalid login or password.");
			}

			var now = DateTime.Now;

			//создаем JWT-токены

			var jwt = new JwtSecurityToken(
				issuer: AuthOptions.ISSUER,
				audience: AuthOptions.AUDIENCE,
				notBefore: now,
				claims: identity.Claims,
				expires: now.Add(new AuthOptions().LIFETIME),
				signingCredentials: new SigningCredentials(
					AuthOptions.GetSymmetricSecurityKey(),
					SecurityAlgorithms.HmacSha256));
			var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
			var response = new
				               {
					               access_token = encodedJwt,
					               username = identity.Name
				               };
			return this.Ok(response);
		}
		

	}
}
