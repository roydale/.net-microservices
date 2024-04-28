using Microsoft.AspNetCore.Mvc;

namespace CommandService.Api.Controllers
{
	public class CommandsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
