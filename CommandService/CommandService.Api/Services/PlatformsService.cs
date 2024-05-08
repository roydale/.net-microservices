using AutoMapper;
using CommandService.Api.Data.Repositories;
using CommandService.Api.Dtos;

namespace CommandService.Api.Services
{
	public class PlatformsService(IPlatformRepository _repository, IMapper _mapper)
	{
		public IEnumerable<PlatformReadDto> GetPlatforms()
		{
			var platforms = _repository.GetAll();
			var platformReadDtos = _mapper.Map<IEnumerable<PlatformReadDto>>(platforms);
			return platformReadDtos;
		}
	}
}
