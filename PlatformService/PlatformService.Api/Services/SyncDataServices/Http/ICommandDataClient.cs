using PlatformService.Api.Dtos;

namespace PlatformService.Api.Services.SyncDataServices.Http
{
	public interface ICommandDataClient
	{
		Task SendPlatformToCommand(PlatformReadDto platform);
	}
}
