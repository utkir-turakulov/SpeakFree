using System;
using System.Collections.Generic;
using System.Text;

namespace SpeakFree.DAL.Models
{
    /// <summary>
    /// Модель сообщение
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Идентификатор сообщения
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Является ли соощение анонимным
        /// </summary>
        public bool IsAnonymous { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedAt { get; set; }

		/// <summary>
		/// Дата удаления
		/// </summary>
		public DateTime DeletedAt { get; set; }

		/// <summary>
		/// Тип сообщения
		/// </summary>
		public MessageType Type { get; set; }

		/// <summary>
		/// Автор сообщения 
		/// </summary>
		public long? AuthorId { get; set; }		
        public User Author { get; set; }
    }
}
