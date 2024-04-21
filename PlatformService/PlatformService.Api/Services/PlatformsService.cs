using AutoMapper;
using PlatformService.Api.Data;
using PlatformService.Api.Dtos;
using PlatformService.Api.Models;

namespace PlatformService.Api.Services
{
	public class PlatformsService(IPlatformRepository _repository, IMapper _mapper)
	{
		public IEnumerable<PlatformReadDto> GetPlatforms()
		{
			var platforms = _repository.GetAll();
			var platformReadDtos = _mapper.Map<IEnumerable<PlatformReadDto>>(platforms);
			return platformReadDtos;
		}

		public PlatformReadDto? GetPlatformById(int id)
		{
			var platform = _repository.GetById(id);
			if (platform != null)
			{
				var platformReadDto = _mapper.Map<PlatformReadDto>(platform);
				return platformReadDto;
			}
			return null;
		}

		public PlatformReadDto CreatePlatform(PlatformCreateDto platformCreateDto)
		{
			var platform = _mapper.Map<Platform>(platformCreateDto);
			_repository.Create(platform);
			_repository.SaveChanges();

			var platformReadDto = _mapper.Map<PlatformReadDto>(platform);
			return platformReadDto;
		}
	}
}
