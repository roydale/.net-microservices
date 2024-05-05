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
		}
	}
}
