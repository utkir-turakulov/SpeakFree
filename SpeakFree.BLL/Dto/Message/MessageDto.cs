using System;
using System.Collections.Generic;
using System.Text;

namespace SpeakFree.BLL.Dto.Message
{
	using System.Runtime.Serialization;

	using SpeakFree.DAL.Models;

	[DataContract]
	public class MessageDto
    {
		[DataMember]
		public long Id { get; set; }

		[DataMember]
		public string Text { get; set; }

		[DataMember]
		public bool IsAnonymous { get; set; }

		[DataMember]
		public DateTime CreatedAt { get; set; }

		[DataMember]
		public DateTime DeletedAt { get; set; }

		[DataMember]
		public string AuthorId { get; set; }
    }
}
