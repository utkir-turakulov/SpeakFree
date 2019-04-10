using System;
using System.Collections.Generic;
using System.Text;

namespace SpeakFree.BLL.Services
{
	using SpeakFree.DAL.Models;
	using System.Threading.Tasks;

	public interface IUserOperationService
	{
		Task<User> Get(string id);

		Task<IEnumerable<User>> GetAll();

		Task<User> GetByLogin(string login);
	}
}
