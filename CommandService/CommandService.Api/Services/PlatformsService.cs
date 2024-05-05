using AutoMapper;
using CommandService.Api.Data.Repositories;
using CommandService.Api.Dtos;
using CommandService.Api.Models;

namespace CommandService.Api.Services
{
	public class PlatformsService(IPlatformRepository _repository, IMapper _mapper)
	{
		//public PlatformReadDto CreatePlatform(PlatformCreateDto platformCreateDto)
		//{
		//	var platform = _mapper.Map<Platform>(platformCreateDto);
		//	_repository.Create(platform);
		//	_repository.SaveChanges();

		//	var platformReadDto = _mapper.Map<PlatformReadDto>(platform);
		//	return platformReadDto;
		//}

		public IEnumerable<PlatformReadDto> GetPlatforms()
		{
			var platforms = _repository.GetAll();
			var platformReadDtos = _mapper.Map<IEnumerable<PlatformReadDto>>(platforms);
			return platformReadDtos;
		}
	}
}
