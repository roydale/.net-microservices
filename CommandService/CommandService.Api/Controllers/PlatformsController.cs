using CommandService.Api.Dtos;
using CommandService.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Api.Controllers
{
	[Route("api/cmd/[controller]")]
	[ApiController]
	public class PlatformsController(PlatformsService _service) : Controller
	{
		[HttpGet]
		public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
		{
			Console.WriteLine("---> Getting Platforms from CommandService...");
			return Ok(_service.GetPlatforms());
		}

		[HttpPost]
		public IActionResult TestInboundConnection()
		{
			Console.WriteLine("---> Inbound POST # Command Service");
			return Ok("Inbound test of Platforms Controller");
		}
	}
}
