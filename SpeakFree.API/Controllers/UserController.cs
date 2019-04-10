using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpeakFree.DAL.Models;

namespace SpeakFree.API.Controllers
{
	using Microsoft.EntityFrameworkCore;

	[Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
	    private UserManager<User> _userManager;

	    private SignInManager<User> _signInManager;

		public UserController(UserManager<User> userManager,
	                          SignInManager<User> signInManager)
		{
			this._userManager = userManager;
			this._signInManager = signInManager;
		}

		/// <summary>
		/// Получить всех пользователей
		/// </summary>
		/// <returns></returns>
		[HttpGet("GetAll")]
	    public async Task<IActionResult> GetAll()
	    {
			var users = await this._userManager.Users.ToListAsync();
			return this.Ok(users);
	    }

		/// <summary>
		/// Получить пользователя по Id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("Get{id}")]
	    public async Task<IActionResult> Get(string id)
	    {
		    var users = await this._userManager.Users.Where(x=> x.Id == id).ToListAsync();
		    return this.Ok(users);
		}

		/// <summary>
		/// Получить пользователя по логину
		/// </summary>
		/// <param name="login"></param>
		/// <returns></returns>
	    [HttpGet("GetByLogin/{login}")]
	    public async Task<IActionResult> GetByLogin(string login)
	    {
		    var users = await this._userManager.Users.Where(x => x.Email == login).ToListAsync();
		    return this.Ok(users);
		}
    }
}