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
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.EntityFrameworkCore;

	using SpeakFree.BLL.Services;

	[Route("/[controller]")]
   // [ApiController]
	[Authorize]
	public class UserController : ControllerBase
    {
	    private UserManager<User> _userManager;

	    private SignInManager<User> _signInManager;

	    private IUserOperationService _userOperationService;

		public UserController(UserManager<User> userManager,
	                          SignInManager<User> signInManager,
	                          IUserOperationService userOperationService)
		{
			this._userManager = userManager;
			this._signInManager = signInManager;
			this._userOperationService = userOperationService;
		}

		/// <summary>
		/// Получить всех пользователей
		/// </summary>
		/// <returns></returns>
		[HttpGet("GetAll")]
		[Authorize]
	    public async Task<IActionResult> GetAll()
		{
			var users = await this._userOperationService.GetAll();
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
			var users = await this._userOperationService.Get(id);
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
			var users = await this._userOperationService.GetByLogin(login);
		    return this.Ok(users);
		}
    }
}