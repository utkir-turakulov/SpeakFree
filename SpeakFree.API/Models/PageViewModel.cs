using System;

namespace SpeakFree.API.Models
{
	/// <summary>
	/// Модель описывающая пагинацию
	/// </summary>
	public class PageViewModel
    {
		/// <summary>
		/// Номер строки
		/// </summary>
		public int PageNumber { get; private set; }

		/// <summary>
		/// Количество страниц
		/// </summary>
		public int TotalPages { get; private set; }

		public PageViewModel(int count, int pageNumber, int pageSize)
		{
			PageNumber = pageNumber;
			TotalPages = (int)Math.Ceiling(count / (double)pageSize);
		}

		public bool HasPreviousPage
		{
			get
			{
				return (PageNumber > 1);
			}
		}

		public bool HasNextPage
		{
			get
			{
				return (PageNumber < TotalPages);
			}
		}
    }
}
