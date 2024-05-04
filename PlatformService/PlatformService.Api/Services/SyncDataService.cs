using PlatformService.Api.Dtos;
using PlatformService.Api.Services.SyncDataServices.Http;

namespace PlatformService.Api.Services
{
	public class SyncDataService(ICommandDataClient _commandDataClient)
	{
		public async Task SendDataToCommandService(PlatformReadDto platformReadDto)
		{
			try
			{
				await _commandDataClient.SendPlatformToCommand(platformReadDto);
			}
			catch (Exception exception)
			{
				Console.WriteLine($"---> Could not send synchronously: {exception.Message}");
			}
		}
	}
}
