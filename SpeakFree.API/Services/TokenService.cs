using Microsoft.AspNetCore.Identity;
using SpeakFree.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SpeakFree.API.Services
{
	public class TokenService
	{

		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;

		private readonly RoleManager<User> _roleManager;

		public TokenService(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<User> roleManager)
		{
			this._userManager = userManager;
			this._signInManager = signInManager;
			this._roleManager = roleManager;
		}

		/// <summary>
		/// Авторизация пользователя
		/// </summary>
		/// <param name="userName"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		public async Task<ClaimsIdentity> GetIdentity(string userName, string password)
		{
			var result = await this._signInManager.PasswordSignInAsync(userName, password, false, false);

			if (result.Succeeded)
			{
				User user = await this._userManager.FindByEmailAsync(userName); //Продумать как проверять 
				List<string> roles = (await this._userManager.GetRolesAsync(user)).ToList();

				var claims = new List<Claim>()
					             {
						             new Claim(ClaimsIdentity.DefaultNameClaimType,user.Email),
						             new Claim(ClaimsIdentity.DefaultRoleClaimType, roles.ToString())
					             };
				ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
					ClaimsIdentity.DefaultRoleClaimType);
				return claimsIdentity;
			}

			return null;
		}
	}
}
