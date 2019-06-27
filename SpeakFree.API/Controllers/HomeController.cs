using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeakFree.API.Controllers
{
	[Route("/[controller]")]
	public class HomeController : Controller
	{
		/// <summary>
		/// Получить домашнюю страницу
		/// </summary>
		/// <returns></returns>
		[HttpGet(Name = "Index")]
		[Route("/[controller]/[action]")]
		
		 public async Task<IActionResult> Index()
		{
			return View();
		}
}
}
