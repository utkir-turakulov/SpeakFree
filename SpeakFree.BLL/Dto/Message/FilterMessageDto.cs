using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using SpeakFree.DAL.Enums;

namespace SpeakFree.BLL.Dto.Message
{
	/// <summary>
	/// Модель фильтрации
	/// </summary>
	[DataContract]
	public class FilterMessageDto
	{
		/// <summary>
		/// По дням
		/// </summary>
		[DataMember]
		public DateFilter DateFilter { get; set; }

		/// <summary>
		/// По типу сообщения
		/// </summary>
		[DataMember]
		public MessageType MessageType { get; set; }

		/// <summary>
		/// По приоритету
		/// </summary>
		[DataMember]
		public Priority Priority { get; set; }

		[DataMember]
		public string ReturnUrl { get; set; }

		[DataMember]
		public int PageNumber { get; set; } = 1;
	}
}
