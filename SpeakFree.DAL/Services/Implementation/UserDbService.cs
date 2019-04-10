using System;
using System.Collections.Generic;
using System.Text;
using SpeakFree.DAL.Models;

namespace SpeakFree.DAL.Services.Implementation
{
	using System.Linq;
	using System.Threading.Tasks;
	using Microsoft.EntityFrameworkCore;

	using SpeakFree.DAL.Context;

	public class UserDbService : IUserDbService
	{
		private SpeakFreeDataContext _context;

		public UserDbService(SpeakFreeDataContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Получить пользователя по id
		/// </summary>
		/// <param name="id"></param>
		/// <returns>User</returns>
		public async Task<User> Get(string id)
		{
			return await this._context.Users.Where(x => x.Id == id).FirstAsync();
		}

		/// <summary>
		/// Получить всех пользователей
		/// </summary>
		/// <returns>IEnumerable<User></returns>
		public async Task<IEnumerable<User>> GetAll()
		{
			return await this._context.Users.ToListAsync();
		}

		/// <summary>
		/// Получить пользователя по логину
		/// </summary>
		/// <param name="login"></param>
		/// <returns></returns>
		public async Task<User> GetByLogin(string login)
		{
			return await this._context.Users.Where(x => x.Email == login).FirstAsync();
		}
	}
}
