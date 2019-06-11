using SpeakFree.BLL.Dto.Message;
using SpeakFree.DAL.Enums;
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

	    Message Get(long id);

	    Task<IEnumerable<Message>> Find(Func<Message, Boolean> predicacte);

		Task<IEnumerable<Message>> Filter(FilterMessageDto data);

		Task<IEnumerable<Message>> FilterByDate(DateFilter data);

		Task<IEnumerable<Message>> FilterByPriority(int data);

		Task<IEnumerable<Message>> FilterByMessageType(int data);

		Task Create(Message item);

	    Task Update(Message item);

	    Task Delete(Message item);
	}
}
