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
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Автор сообщения 
        /// </summary>
        public User Author { get; set; }
    }
}
