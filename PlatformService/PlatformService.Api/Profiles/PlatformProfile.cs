using AutoMapper;
using PlatformService.Api.Dtos;
using PlatformService.Api.Models;

namespace PlatformService.Api.Profiles
{
	public class PlatformProfile : Profile
	{
		public PlatformProfile()
		{
			// Source -> Target
			CreateMap<Platform, PlatformReadDto>();
			CreateMap<PlatformCreateDto, Platform>();
			CreateMap<PlatformReadDto, PlatformPublishedDto>();
		}
	}
}
