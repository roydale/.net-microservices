using AutoMapper;
using PlatformService.Api.Dtos;
using PlatformService.Api.Services.AsyncDataServices;
using PlatformService.Api.Services.SyncDataServices.Http;

namespace PlatformService.Api.Services
{
	public class SyncDataService(ICommandDataClient _commandDataClient,
		IMapper _mapper,
		IMessageBusClient _messageBusClient)
	{
		public async Task SendDataToCommandService(PlatformReadDto platformReadDto)
		{
			// Send Synchronous message
			try
			{
				await _commandDataClient.SendPlatformToCommand(platformReadDto);
			}
			catch (Exception exception)
			{
				Console.WriteLine("---> Could not send synchronously: {0}", exception.Message);
			}

			// Send Asynchronous message
			try
			{
				var platformPublishedDto = _mapper.Map<PlatformPublishedDto>(platformReadDto);
				platformPublishedDto.Event = "Platform_Published";
				_messageBusClient.PublishNewPlatform(platformPublishedDto);

			}
			catch (Exception exception)
			{
				Console.WriteLine("---> Could not send asynchronously: {0}", exception.Message);
			}
		}
	}
}
