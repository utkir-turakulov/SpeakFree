using SpeakFree.DAL.Models;
using System.Collections.Generic;

namespace SpeakFree.API.Models
{
	public class TaskViewModel
    {
		public List<Message> Tasks = new List<Message>();
		public User CurrentUser{ get; set; }
		public PageViewModel PageViewModel { get; set; }
    }
}
