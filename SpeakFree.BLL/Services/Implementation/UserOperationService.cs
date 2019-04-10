using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SpeakFree.DAL.Models;

namespace SpeakFree.BLL.Services.Implementation
{
	using SpeakFree.DAL.Services;

	public class UserOperationService : IUserOperationService
	{
		private IUserDbService _userDbService;

		public UserOperationService(IUserDbService userDbService)
		{
			this._userDbService = userDbService;
		}

		/// <summary>
		/// Получить по id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Task<User> Get(string id)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Получить всех пользователей
		/// </summary>
		/// <returns></returns>
		public Task<IEnumerable<User>> GetAll()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Получить пользователя по логину
		/// </summary>
		/// <param name="login"></param>
		/// <returns></returns>
		public Task<User> GetByLogin(string login)
		{
			throw new NotImplementedException();
		}
	}
}
