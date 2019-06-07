using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpeakFree.DAL.Context;
using SpeakFree.DAL.Models;

namespace SpeakFree.DAL.Services.Implementation
{
	/// <summary>
	/// Сервис управления сообщениями
	/// </summary>
	public class MessageDbService : IMessageDbService
	{
		private SpeakFreeDataContext _context;

		public MessageDbService(SpeakFreeDataContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Создать сообщение
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public async Task Create(Message item)
		{
			await this._context.Messages.AddAsync(item);
			await this._context.SaveChangesAsync();
		}

		/// <summary>
		/// Удалить сообщение
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public async Task Delete(Message item)
		{
			this._context.Messages.Remove(item);
			await _context.SaveChangesAsync();
		}

		/// <summary>
		/// Найти сообщения
		/// </summary>
		/// <param name="predicacte"></param>
		/// <returns></returns>
		public IEnumerable<Message> Find(Func<Message, bool> predicacte)
		{
			return _context.Messages.Where(predicacte).ToList();
		}

		/// <summary>
		/// Получить сообщение по id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Message Get(long id)
		{
			return this._context.Messages.Find(id);
		}

		/// <summary>
		/// Получить все сообщения
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Message> GetAll()
		{
			return this._context.Messages;
		}

		/// <summary>
		/// Обновить сообщение
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public async Task Update(Message item)
		{
			this._context.Entry(item).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}
	}
}
