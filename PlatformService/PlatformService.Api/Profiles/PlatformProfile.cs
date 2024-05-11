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
			CreateMap<Platform, GrpcPlatformModel>()
				.ForMember(target => target.PlatformId, options => options.MapFrom(source => source.Id))
				.ForMember(target => target.Name, options => options.MapFrom(source => source.Name))
				.ForMember(target => target.Publisher, options => options.MapFrom(source => source.Publisher));
		}
	}
}
