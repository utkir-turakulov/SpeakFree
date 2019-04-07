using SpeakFree.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpeakFree.BLL.Services
{
    public interface IMessageService
    {
	    Task<IEnumerable<Message>> GetAll();

	    Message Get(int id);

	    IEnumerable<Message> Find(Func<Message, Boolean> predicacte);

	    Task Create(Message item);

	    Task Update(Message item);

	    Task Delete(Message item);
	}
}
