using CommandService.Api.Models;

namespace CommandService.Api.Data.Repositories
{
	public interface ICommandRepository : IBaseRepository
    {
        IEnumerable<Command> GetAllByPlatformId(int platformId);

        Command? GetByIdPlatformId(int id, int platformId);

        void Create(Command command, int platformId);
    }
}
