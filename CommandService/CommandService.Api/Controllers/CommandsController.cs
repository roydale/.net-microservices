using CommandService.Api.Dtos;
using CommandService.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Api.Controllers
{
	[Route("api/cmd/platforms/{platformId}/[controller]")]
	[ApiController]
	public class CommandsController(CommandsService _service) : Controller
	{
		[HttpGet]
		public ActionResult<IEnumerable<CommandReadDto>> GetCommandsByPlatformId(int platformId)
		{
			Console.WriteLine($"---> Getting All Commands by PlatformId: {platformId}");
			var commands = _service.GetCommandsByPlatformId(platformId);
			return commands.Any() ? Ok(commands) : NotFound();
		}

		[HttpGet("{commandId}", Name = "GetCommandByIdPlatformId")]
		public ActionResult<CommandReadDto> GetCommandByIdPlatformId(int commandId, int platformId)
		{
			Console.WriteLine($"---> Getting Command by Command Id: {commandId}, Platform Id: {platformId}");
			var command = _service.GetCommandByIdPlatformId(commandId, platformId);
			return command != null ? Ok(command) : NotFound();
		}

		[HttpPost]
		public ActionResult<CommandReadDto> CreateCommandForPlatform(CommandCreateDto commandCreateDto, int platformId)
		{
			Console.WriteLine($"---> Creating Command for Platform: {platformId}");
			var command = _service.CreateCommandForPlatform(commandCreateDto, platformId);
			return command != null
				? CreatedAtRoute(nameof(GetCommandByIdPlatformId), new { platformId, commandId = command.Id }, command)
				: NotFound();
		}
	}
}
