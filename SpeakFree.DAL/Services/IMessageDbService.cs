using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpeakFree.DAL.Services
{
	using SpeakFree.DAL.Models;

	public interface IMessageDbService
    {
	    IEnumerable<Message> GetAll();

	    Message Get(long id);

	    IEnumerable<Message> Find(Func<Message, Boolean> predicacte);

	    Task Create(Message item);

	    Task Update(Message item);

	    Task Delete(Message item);
	}
}
