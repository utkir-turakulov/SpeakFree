using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SpeakFree.DAL.Models;
using Mapster;

namespace SpeakFree.BLL.Services.Implementation
{
	using SpeakFree.BLL.Dto.User;
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
		public async Task<UserDto> Get(string id)
		{
			var user = (await _userDbService.Get(id)).Adapt<UserDto>();
			return user;
		}

		/// <summary>
		/// Получить всех пользователей
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<UserDto>> GetAll()
		{
			var users = (await this._userDbService.GetAll()).Adapt<IEnumerable<UserDto>>();
			
			return users;
		}

		/// <summary>
		/// Получить пользователя по логину
		/// </summary>
		/// <param name="login"></param>
		/// <returns></returns>
		public async Task<UserDto> GetByLogin(string login)
		{
			var user = (await _userDbService.GetByLogin(login)).Adapt<UserDto>();
			return user;
		}
	}
}
