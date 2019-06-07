using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SpeakFree.DAL.Models
{
	[DataContract]
	public class User: IdentityUser
    {

		/// <summary>
		/// Имя
		/// </summary>
		[DataMember]
		public string Name { get; set; }

		/// <summary>
		/// Отчество
		/// </summary>
		[DataMember]
		public string Patronymic { get; set; }

		/// <summary>
		/// Фамилия
		/// </summary>
		[DataMember]
		public string Surename { get; set; }

		public List<Message> Messages { get; set; }
	}
}
