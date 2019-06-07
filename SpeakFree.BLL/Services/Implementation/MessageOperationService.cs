using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SpeakFree.DAL.Models;

namespace SpeakFree.BLL.Services.Implementation
{
	using SpeakFree.DAL.Services;
	using SpeakFree.DAL.Services.Implementation;

	public class MessageOperationService : IMessageService
	{
		private readonly IMessageDbService messageService;

		public MessageOperationService(IMessageDbService _messageService)
		{
			this.messageService = _messageService;
		}

		public async Task Create(Message item)
		{
			await this.messageService.Create(item);
		}

		public async Task Delete(Message item)
		{
			await this.messageService.Delete(item);
		}

		public IEnumerable<Message> Find(Func<Message, bool> predicacte)
		{
			return this.messageService.Find(predicacte);
		}

		public Message Get(long id)
		{
			return this.messageService.Get(id);
		}

		public async Task<IEnumerable<Message>> GetAll()
		{
			return this.messageService.GetAll();
		}

		public async Task Update(Message item)
		{
			await this.messageService.Update(item);
		}
	}
}
