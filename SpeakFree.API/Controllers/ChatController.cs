using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpeakFree.API.Models;
using SpeakFree.DAL.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SpeakFree.API.Controllers
{
    [Route("/[controller]")]
    public class ChatController : Controller
    {
		UserManager<User> _userManager;

		public ChatController(UserManager<User> userManager)
		{
			_userManager = userManager;
		}

        // GET: /<controller>/
        [HttpGet("GetChat")]
		//[Route("GetChat")]
		public async Task<IActionResult> GetChat()
        {
			var user = await _userManager.FindByEmailAsync(User.Identity.Name);
			ChatViewModel model = new ChatViewModel()
			{
				User = user
			};

            return View("Index",model);
        }
    }
}
