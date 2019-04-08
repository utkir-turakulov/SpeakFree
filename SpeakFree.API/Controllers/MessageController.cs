using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SpeakFree.API.Controllers
{
	using Microsoft.AspNetCore.Identity;
	using SpeakFree.BLL.Dto.Message;
	using SpeakFree.BLL.Services;
	using SpeakFree.BLL.Services.Implementation;
	using SpeakFree.DAL.Models;

	/// <summary>
	/// Сообщения
	/// </summary>
    [Route("api/[controller]")]
    public class MessageController : Controller
	{
		private readonly IMessageService _messageOperationService;
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;

		public MessageController(
			IMessageService messageOperationService,
			UserManager<User> userManager,
			SignInManager<User> signInManager
			)
		{
			this._messageOperationService = messageOperationService;
			this._userManager = userManager;
			this._signInManager = signInManager;
		}

		/// <summary>
		/// Получить все
		/// </summary>
		/// <returns></returns>
		// GET: api/<controller>
		[HttpGet]
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
        [HttpGet("{id}")]
        public async Task<Message> Get(int id)
        {
            return _messageOperationService.Get(id);
        }

		/// <summary>
		/// Создать
		/// </summary>
		/// <param name="value"></param>
        // POST api/<controller>
        [HttpPost("Create")]
        public async Task Create([FromBody]MessageDto value)
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
										  Author = user,
										  CreatedAt = value.CreatedAt,
										  IsAnonymous = value.IsAnonymous,
										  Text = value.Text
					                  };
				await this._messageOperationService.Create(message);
			}			
		}

		/// <summary>
		/// Изменить
		/// </summary>
		/// <param name="message"></param>
        // Post api/<controller>/5
        [HttpPost("{id}")]
        public async Task Edit([FromBody]MessageDto message)
        {
	        if (message != null)
	        {
		        User user = await this._userManager.FindByIdAsync(message.AuthorId.ToString());
				Message msg = _messageOperationService.Find(x => x.Id == message.Id).First();

				msg.CreatedAt = message.CreatedAt;
				msg.DeletedAt = message.DeletedAt;
				msg.Author = user;
				msg.IsAnonymous = message.IsAnonymous;
				msg.Text = msg.Text;

				await this._messageOperationService.Update(msg);
	        }
        }
		
		/// <summary>
		/// Удалить
		/// </summary>
		/// <param name="id"></param>
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task DeleteAsync(int? id)
        {
	        if (id != null)
	        {
		        Message message = _messageOperationService.Find(x => x.Id == id).First();
				await this._messageOperationService.Delete(message);
	        }
        }
    }
}
