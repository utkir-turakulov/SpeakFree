using System;
using System.Runtime.Serialization;

namespace SpeakFree.DAL.Models
{
	/// <summary>
	/// Модель сообщение
	/// </summary>
	[DataContract]
	public class Message
    {
        /// <summary>
        /// Идентификатор сообщения
        /// </summary>
		[DataMember]
        public long Id { get; set; }

		/// <summary>
		/// Текст сообщения
		/// </summary>
		[DataMember]
		public string Text { get; set; }

		/// <summary>
		/// Заголовок сообщения
		/// </summary>
		[DataMember]
		public string Title { get; set; }

		/// <summary>
		/// Является ли соощение анонимным
		/// </summary>
		[DataMember]
		public bool IsAnonymous { get; set; }

		/// <summary>
		/// Дата создания
		/// </summary>
		[DataMember]
		public DateTime CreatedAt { get; set; }

		/// <summary>
		/// Дата удаления
		/// </summary>
		[DataMember]
		public DateTime DeletedAt { get; set; }

		/// <summary>
		/// Тип сообщения
		/// </summary>
		[DataMember]
		public MessageType Type { get; set; }

		/// <summary>
		/// Приоритет
		/// </summary>
		[DataMember]
		public Priority Priority { get; set; }

		/// <summary>
		/// Автор сообщения 
		/// </summary>
		public string UserId { get; set; }
		[DataMember]
		public User User { get; set; }
    }
}
