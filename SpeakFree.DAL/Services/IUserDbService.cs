using SpeakFree.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpeakFree.DAL.Services
{
    public interface IUserDbService
    {
	    Task<IEnumerable<User>> GetAll();

	    Task<User> Get(string id);

	    Task<User> GetByLogin(string login);
    }
}
