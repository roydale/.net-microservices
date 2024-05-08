using AutoMapper;
using CommandService.Api.Dtos;
using CommandService.Api.Models;

namespace CommandService.Api.Profiles
{
	public class CommandProfile : Profile
	{
		public CommandProfile()
		{
			// Source -> Target
			CreateMap<Command, CommandReadDto>();
			CreateMap<CommandCreateDto, Command>();
			CreateMap<PlatformPublishedDto, Platform>()
				.ForMember(destination => destination.ExternalId,
					option => option.MapFrom(source => source.Id));
		}
	}
}
