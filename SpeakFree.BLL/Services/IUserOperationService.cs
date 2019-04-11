using System;
using System.Collections.Generic;
using System.Text;

namespace SpeakFree.BLL.Services
{
	using SpeakFree.DAL.Models;
	using System.Threading.Tasks;

	using SpeakFree.BLL.Dto.User;

	public interface IUserOperationService
	{
		Task<UserDto> Get(string id);

		Task<IEnumerable<UserDto>> GetAll();

		Task<UserDto> GetByLogin(string login);
	}
}
