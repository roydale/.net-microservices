using Microsoft.AspNetCore.Mvc;
using PlatformService.Api.Dtos;
using PlatformService.Api.Services;

namespace PlatformService.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PlatformsController(PlatformsService _service) : Controller
	{

		[HttpGet]
		public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
		{
			Console.WriteLine("Getting Platforms...");
			return Ok(_service.GetPlatforms());
		}

		[HttpGet("{id}", Name = "GetPlatformById")]
		public ActionResult<PlatformReadDto> GetPlatformById([FromRoute] int id)
		{
			Console.WriteLine("Getting Platform By Id...");
			var platformReadDto = _service.GetPlatformById(id);
			return platformReadDto != null ? Ok(platformReadDto) : NotFound();
		}

		[HttpPost]
		public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto platform)
		{
			Console.WriteLine("Creating Platform...");
			var platformReadDto = _service.CreatePlatform(platform);
			return CreatedAtRoute(nameof(GetPlatformById), new { platformReadDto.Id }, platformReadDto);
		}
	}
}
