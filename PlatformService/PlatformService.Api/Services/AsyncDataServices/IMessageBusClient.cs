using PlatformService.Api.Dtos;

namespace PlatformService.Api.Services.AsyncDataServices
{
	public interface IMessageBusClient : IDisposable
	{
		void PublishNewPlatform(PlatformPublishedDto platformPublisedDto);
	}
}
