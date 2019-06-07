using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpeakFree.API.Models;
using SpeakFree.BLL.Services;
using SpeakFree.DAL.Models;
using SpeakFree.BLL.Dto.Message;
using SpeakFree.BLL.Services;
using SpeakFree.BLL.Services.Implementation;
using SpeakFree.DAL.Models;
using Microsoft.AspNetCore.Authorization;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SpeakFree.API.Controllers
{
    [Route("/[controller]")]
    public class TaskBoardController : Controller
    {
		private readonly IMessageService _messageOperationService;
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly IUserOperationService _userOperationService;
		private readonly int PAGE_SIZE;

		public TaskBoardController(
			IMessageService messageOperationService,
			IUserOperationService userOperationService,
			UserManager<User> userManager,
			SignInManager<User> signInManager)
		{
			this._messageOperationService = messageOperationService;
			this._userManager = userManager;
			this._signInManager = signInManager;
			this._userOperationService = userOperationService;
			PAGE_SIZE = 10;
		}

		// GET: /<controller>/
		[HttpGet("GetTasks")]
		[Route("GetTasks")]
        public async Task<IActionResult> Index(int page = 1)
        {
			var messageSourse = (await _messageOperationService.GetAll()).ToList();
			var count = messageSourse.Count();
			var result = messageSourse.Skip((page-1) * PAGE_SIZE).Take(PAGE_SIZE).ToList();
			var user = await _userManager.FindByEmailAsync(User.Identity.Name);

			for (int i=0; i< result.Count;i++)
			{
				if (result[i].UserId != null)
				{
					if (result[i].User == null)
					{
						result[i].User = await _userManager.FindByIdAsync(result[i].UserId.ToString());
					}
				}
			}


			PageViewModel pageViewModel = new PageViewModel(count, page, PAGE_SIZE);
			TaskViewModel model = new TaskViewModel
			{
				Tasks = result,
				CurrentUser = user,
				PageViewModel = pageViewModel
			};

			return View(model);
        }


		/// <summary>
		/// Получить все
		/// </summary>
		/// <returns></returns>
		// GET: api/<controller>
		[HttpGet("GetAll")]
		public async Task<IEnumerable<Message>> Get()
		{
			return await _messageOperationService.GetAll();
		}

		/// <summary>
		/// Получить по id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		// GET api/<controller>/5
		[HttpGet("Get/{id}")]
		[AllowAnonymous]
		public async Task<IActionResult> Get(long id)
		{
			var message = _messageOperationService.Get(id);

			if (message.UserId != null)
			{
				if (message.User == null)
				{
					message.User = await _userManager.FindByIdAsync(message.UserId.ToString());
				}
			}

			return Ok(message);
		}

		/// <summary>
		/// Создать
		/// </summary>
		/// <param name="value"></param>
		// POST api/<controller>
		[HttpPost("Create")]
		public async Task<IActionResult> Create(MessageDto value)
		{
			if (value != null)
			{
				User user = null;
				if (!value.IsAnonymous)
				{
					user = await this._userManager.FindByIdAsync(value.AuthorId.ToString());
				}

				Message message = new Message()
				{
					User = user,
					CreatedAt = value.CreatedAt == DateTime.MinValue ? DateTime.Now : value.CreatedAt,
					IsAnonymous = value.IsAnonymous,
					Text = value.Text,
					Priority = value.Priority,
					Title = value.Title,
					Type = value.Type
				};
				await this._messageOperationService.Create(message);
			}
			if (!string.IsNullOrEmpty(value.ReturnUrl))
			{
				return RedirectToAction(value.ReturnUrl.Split("/")[1], value.ReturnUrl.Split("/")[0]);
			}

			return View("Index");
		}

		/// <summary>
		/// Изменить
		/// </summary>
		/// <param name="message"></param>
		// Post api/<controller>/5
		[HttpPost("Edit")]
		public async Task<IActionResult> Edit(MessageDto message)
		{
			if (message != null)
			{
				User user = null;

				if (!message.IsAnonymous)
				{
					user = await this._userManager.FindByIdAsync(message.AuthorId.ToString());
				}

				Message msg = _messageOperationService.Find(x => x.Id == message.Id).First();

				msg.CreatedAt = message.CreatedAt == DateTime.MinValue ? DateTime.Now : message.CreatedAt;
				msg.DeletedAt = message.DeletedAt;
				msg.User = user;
				msg.IsAnonymous = message.IsAnonymous;
				msg.Text = message.Text;
				msg.Title = message.Title;
				msg.Priority = message.Priority;
				msg.Type = message.Type;
				
				await this._messageOperationService.Update(msg);
			}

			if (!string.IsNullOrEmpty(message.ReturnUrl))
			{
				return RedirectToAction(message.ReturnUrl.Split("/")[1], message.ReturnUrl.Split("/")[0]);
			}
			return View("Index");
		}

		/// <summary>
		/// Удалить
		/// </summary>
		/// <param name="messageId"></param>
		// DELETE api/<controller>/5
		[HttpPost("Delete")]
		public async Task<IActionResult> Delete(int messageId,string returnUrl)
		{
			if (messageId != null)
			{
				Message message = _messageOperationService.Find(x => x.Id == messageId).First();
				await this._messageOperationService.Delete(message);
			}

			if (!string.IsNullOrEmpty(returnUrl))
			{
				return RedirectToAction(returnUrl.Split("/")[1], returnUrl.Split("/")[0]);
			}

			return View("Index");
		}

	}
}
