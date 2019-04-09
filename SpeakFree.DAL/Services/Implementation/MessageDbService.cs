using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpeakFree.DAL.Context;
using SpeakFree.DAL.Models;

namespace SpeakFree.DAL.Services.Implementation
{
	public class MessageDbService : IMessageDbService
	{
		private SpeakFreeDataContext _context;

		public MessageDbService(SpeakFreeDataContext context)
		{
			_context = context;
		}

		public async Task Create(Message item)
		{
			await this._context.Messages.AddAsync(item);
			await this._context.SaveChangesAsync();
		}

		public async Task Delete(Message item)
		{
			this._context.Messages.Remove(item);
			await _context.SaveChangesAsync();
		}

		public IEnumerable<Message> Find(Func<Message, bool> predicacte)
		{
			return _context.Messages.Where(predicacte).ToList();
		}

		public Message Get(int id)
		{
			return this._context.Messages.Find(id);
		}

		public IEnumerable<Message> GetAll()
		{
			return this._context.Messages;
		}

		public async Task Update(Message item)
		{
			this._context.Entry(item).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}
	}
}
