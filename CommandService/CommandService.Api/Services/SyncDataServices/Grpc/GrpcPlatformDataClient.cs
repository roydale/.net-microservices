using AutoMapper;
using CommandService.Api.Models;
using Grpc.Net.Client;
using PlatformService.Api;

namespace CommandService.Api.Services.SyncDataServices.Grpc
{
	public class GrpcPlatformDataClient(IConfiguration _configuration, IMapper _mapper) : IPlatformDataClient
	{
		public IEnumerable<Platform> ReturnAllPlatforms()
		{
			Console.WriteLine("---> Calling GRPC Service {0}", _configuration["GrpcPlatform"]);

			var channel = GrpcChannel.ForAddress(_configuration["GrpcPlatform"]!);
			var client = new GrpcPlatform.GrpcPlatformClient(channel);
			var request = new GetAllRequest();

			try
			{
				var reply = client.GetAllPlatforms(request);
				return _mapper.Map<IEnumerable<Platform>>(reply.Platform);
			}
			catch (Exception ex)
			{
				Console.WriteLine("---> Could not call GRPC Server, {0}", ex.ToString());
				return [];
			}
		}
	}
}
