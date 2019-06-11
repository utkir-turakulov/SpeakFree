using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SpeakFree.DAL.Models;
using SpeakFree.BLL.Dto.Message;

namespace SpeakFree.BLL.Services.Implementation
{
	using SpeakFree.DAL.Enums;
	using SpeakFree.DAL.Services;
	using SpeakFree.DAL.Services.Implementation;

	/// <summary>
	/// Сервис управления сообщениями
	/// </summary>
	public class MessageOperationService : IMessageService
	{
		private readonly IMessageDbService messageService;

		public MessageOperationService(IMessageDbService _messageService)
		{
			this.messageService = _messageService;
		}

		/// <summary>
		/// Создать
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public async Task Create(Message item)
		{
			await this.messageService.Create(item);
		}

		/// <summary>
		/// Удалить
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public async Task Delete(Message item)
		{
			await this.messageService.Delete(item);
		}

		/// <summary>
		/// Найти 
		/// </summary>
		/// <param name="predicacte"></param>
		/// <returns></returns>
		public async Task<IEnumerable<Message>> Find(Func<Message, bool> predicacte)
		{
			return await this.messageService.Find(predicacte);
		}

		/// <summary>
		/// Получить по id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Message Get(long id)
		{
			return this.messageService.Get(id);
		}

		/// <summary>
		/// Получить все сообщения
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<Message>> GetAll()
		{
			return await this.messageService.GetAll();
		}

		/// <summary>
		/// Редактировать
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public async Task Update(Message item)
		{
			await this.messageService.Update(item);
		}

		/// <summary>
		/// Фильтровать сообщение
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public async Task<IEnumerable<Message>> Filter(FilterMessageDto data)
		{
			IEnumerable<Message> messages = null;

			if (data.DateFilter != 0)
			{
				switch (data.DateFilter)
				{
					case DateFilter.All:
						messages = await messageService.GetAll();
						break;

					case DateFilter.ThisMonth:
						messages = (await messageService.Find(message => message.CreatedAt.Month == DateTime.Now.Month && message.CreatedAt.Year == DateTime.Now.Year));
						break;

					case DateFilter.ThisWeek:
						var startDate = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek - 1);
						var endDate = DateTime.Now.AddDays(7 - (int)DateTime.Now.DayOfWeek);

						messages = (await messageService.Find(message => message.CreatedAt.Month == DateTime.Now.Month &&
																		message.CreatedAt.Year == DateTime.Now.Year &&
																		message.CreatedAt.Day >= startDate.Day &&
																		message.CreatedAt.Day <= endDate.Day));
						break;

					case DateFilter.ThisYear:
						messages = (await messageService.Find(message => message.CreatedAt.Year == DateTime.Now.Year));
						break;

					case DateFilter.Today:
						messages = (await messageService.Find(message => message.CreatedAt.Day == DateTime.Now.Day &&
																					message.CreatedAt.Month == DateTime.Now.Month &&
																					message.CreatedAt.Year == DateTime.Now.Year));
						break;

					case DateFilter.Yesterday:
						messages = (await messageService.Find(message => message.CreatedAt.Day == DateTime.Now.AddDays(1).Day &&
																					message.CreatedAt.Month == DateTime.Now.Month &&
																					message.CreatedAt.Year == DateTime.Now.Year));
						break;

					default:
						messages = await messageService.GetAll();
						break;
				}
			}

			if (data.MessageType > 0 && (int) data.MessageType <=3 )
			{
				messages = await messageService.Find(message => message.Type == data.MessageType);
			}
			
			if((int)data.MessageType > 3)
			{
				messages = await messageService.GetAll();
			}

			if (data.Priority > 0 && (int)data.Priority <= 4)
			{
				messages = await messageService.Find(message => message.Priority == data.Priority);									
			}

			if((int)data.Priority > 4)
			{
				messages = await messageService.GetAll();
			}

			return messages;
		}

		public async Task<IEnumerable<Message>> FilterByDate(DateFilter data)
		{
			IEnumerable<Message> messages = null;

			switch (data)
			{
				case  DateFilter.All:
					messages = await messageService.GetAll();
					break;

				case DateFilter.ThisMonth:
					messages = (await messageService.Find(message => message.CreatedAt.Month == DateTime.Now.Month && message.CreatedAt.Year == DateTime.Now.Year));
					break;

				case DateFilter.ThisWeek:
					var startDate = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek+1);
					var endDate = DateTime.Now.AddDays(7 - (int)DateTime.Now.DayOfWeek);

					messages = (await messageService.Find(message => message.CreatedAt.Month == DateTime.Now.Month &&
																	message.CreatedAt.Year == DateTime.Now.Year &&
																	message.CreatedAt.Day >= startDate.Day &&
																	message.CreatedAt.Day <= endDate.Day)); 
					break;

				case DateFilter.ThisYear:
					messages = (await messageService.Find(message => message.CreatedAt.Year == DateTime.Now.Year));
					break;

				case DateFilter.Today:
					messages = (await messageService.Find(message => message.CreatedAt.Day == DateTime.Now.Day &&
																				message.CreatedAt.Month == DateTime.Now.Month &&
																				message.CreatedAt.Year == DateTime.Now.Year));
					break;

				case DateFilter.Yesterday:
					messages = (await messageService.Find(message => message.CreatedAt.Day == DateTime.Now.AddDays(-1).Day &&
																				message.CreatedAt.Month == DateTime.Now.Month &&
																				message.CreatedAt.Year == DateTime.Now.Year));
					break;

				default:
					messages = await messageService.GetAll();
					break;
			}

			return messages;
		}

		public async Task<IEnumerable<Message>> FilterByPriority(int data)
		{
			IEnumerable<Message> messages = null;

			switch (data)
			{
				case (int)Priority.Blocker:
					messages = await messageService.Find(message => message.Priority == Priority.Blocker);
					break;

				case (int)Priority.High:
					messages = await messageService.Find(message => message.Priority == Priority.High);
					break;

				case (int)Priority.Low:
					messages = await messageService.Find(message => message.Priority == Priority.Low);
					break;

				case (int)Priority.Normal:
					messages = await messageService.Find(message => message.Priority == Priority.Normal);
					break;

				default:
					messages = await messageService.GetAll();
					break;
			}

			return messages;
		}

		public async Task<IEnumerable<Message>> FilterByMessageType(int data)
		{
			IEnumerable<Message> messages = null;

			switch (data)
			{
				case (int)MessageType.Issue:
					messages = await messageService.Find(message => message.Type == MessageType.Issue);
					break;

				case (int)MessageType.Offer:
					messages = await messageService.Find(message => message.Type == MessageType.Offer);
					break;

				case (int)MessageType.Question:
					messages = await messageService.Find(message => message.Type == MessageType.Question);
					break;

				default:
					messages = await messageService.GetAll();
					break;
			}

			return messages;
		}

		/*public Task<IEnumerable<Message>> FilterByDate(DateFilter data)
		{
			throw new NotImplementedException();
		}*/
	}
}
