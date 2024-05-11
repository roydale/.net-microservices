using CommandService.Api.Models;

namespace CommandService.Api.Services.SyncDataServices.Grpc
{
	public interface IPlatformDataClient
	{
		IEnumerable<Platform> ReturnAllPlatforms();
	}
}
