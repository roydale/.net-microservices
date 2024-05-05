using AutoMapper;
using CommandService.Api.Data.Repositories;
using CommandService.Api.Dtos;
using CommandService.Api.Models;

namespace CommandService.Api.Services
{
	public class CommandsService(ICommandRepository _commandRepository,
								 IPlatformRepository _platformRepository,
								 IMapper _mapper)
	{
		public IEnumerable<CommandReadDto> GetCommandsByPlatformId(int platformId)
		{
			IEnumerable<CommandReadDto> commandReadDtos = [];
			if (!_platformRepository.IsExist(platformId))
			{
				return commandReadDtos;
			}

			var commands = _commandRepository.GetAllByPlatformId(platformId);
			commandReadDtos = _mapper.Map<IEnumerable<CommandReadDto>>(commands);
			return commandReadDtos;
		}

		public CommandReadDto? GetCommandByIdPlatformId(int commandId, int platformId)
		{
			if (!_platformRepository.IsExist(platformId))
			{
				return null;
			}

			var command = _commandRepository.GetByIdPlatformId(commandId, platformId);
			if (command == null)
			{
				return null;
			}

			var commandReadDto = _mapper.Map<CommandReadDto>(command);
			return commandReadDto;
		}

		public CommandReadDto? CreateCommandForPlatform(CommandCreateDto commandCreateDto, int platformId)
		{
			if (!_platformRepository.IsExist(platformId))
			{
				return null;
			}

			var command = _mapper.Map<Command>(commandCreateDto);
			_commandRepository.Create(command, platformId);
			_commandRepository.SaveChanges();

			var commandReadDto = _mapper.Map<CommandReadDto>(command);
			return commandReadDto;
		}
	}
}
