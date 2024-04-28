using Microsoft.AspNetCore.Mvc;

namespace CommandService.Api.Controllers
{
	[Route("api/cmd/[controller]")]
	[ApiController]
	public class PlatformsController : Controller
	{
		public PlatformsController() { }

		[HttpPost]
		public IActionResult TestInboundConnection()
		{
			Console.WriteLine("--> Inbound POST # Command Service");
			return Ok("Inbound test of Platforms Controller");
		}
	}
}
