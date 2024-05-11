using AutoMapper;
using CommandService.Api.Dtos;
using CommandService.Api.Models;
using PlatformService.Api;

namespace CommandService.Api.Profiles
{
	public class PlatformProfile : Profile
	{
		public PlatformProfile()
		{
			// Source -> Target
			CreateMap<Platform, PlatformReadDto>();
			CreateMap<PlatformPublishedDto, Platform>()
				.ForMember(target => target.ExternalId, option => option.MapFrom(source => source.Id));
			CreateMap<GrpcPlatformModel, Platform>()
				.ForMember(target => target.ExternalId, option => option.MapFrom(source => source.PlatformId))
				.ForMember(target => target.Name, option => option.MapFrom(source => source.Name))
				.ForMember(target => target.Commands, option => option.Ignore());
		}
	}
}
