using AutoMapper;
using Grpc.Core;
using PlatformService.Api.Data;

namespace PlatformService.Api.Services.SyncDataServices.Grpc
{
	public class GrpcPlatformService(IPlatformRepository _repository, IMapper _mapper) : GrpcPlatform.GrpcPlatformBase
	{
		public override Task<PlatformResponse> GetAllPlatforms(GetAllRequest request, ServerCallContext context)
		{
			var response = new PlatformResponse();
			var platforms = _repository.GetAll();

			foreach (var platform in platforms)
			{
				response.Platform.Add(_mapper.Map<GrpcPlatformModel>(platform));
			}

			return Task.FromResult(response);
		}
	}
}
