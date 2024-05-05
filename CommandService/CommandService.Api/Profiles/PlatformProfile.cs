using AutoMapper;
using CommandService.Api.Dtos;
using CommandService.Api.Models;

namespace CommandService.Api.Profiles
{
	public class PlatformProfile : Profile
	{
		public PlatformProfile()
		{
			// Source -> Target
			CreateMap<Platform, PlatformReadDto>();
			//CreateMap<PlatformCreateDto, Platform>();
		}
	}
}
