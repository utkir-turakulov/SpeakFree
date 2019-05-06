using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeakFree.API.Controllers
{
	[Route("/[controller]")]
	public class HomeController: Controller
	{
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			return View();
		}
    }
}
