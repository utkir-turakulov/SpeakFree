using System;
using System.Collections.Generic;
using System.Text;

namespace SpeakFree.DAL.Enums
{
	/// <summary>
	/// Фильтр по датам
	/// </summary>
    public enum DateFilter
    {
		/// <summary>
		/// Вся история сообщений
		/// </summary>
		All = 1,

		/// <summary>
		/// За сегодня
		/// </summary>
		Today = 2,

		/// <summary>
		/// Вчера
		/// </summary>
		Yesterday = 3,

		/// <summary>
		/// На этой неделе
		/// </summary>
		ThisWeek = 4,

		/// <summary>
		/// В этом месяце
		/// </summary>
		ThisMonth = 5,

		/// <summary>
		/// В этом году
		/// </summary>
		ThisYear = 6
    }
}
