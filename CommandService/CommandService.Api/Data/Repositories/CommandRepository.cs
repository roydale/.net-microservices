using CommandService.Api.Models;

namespace CommandService.Api.Data.Repositories
{
	public class CommandRepository(AppDbContext context) : BaseRepository(context), ICommandRepository
    {
        private readonly AppDbContext _context = context;

        public void Create(Command command, int platformId)
        {
            ArgumentNullException.ThrowIfNull(command);
            command.PlatformId = platformId;
            _context.Commands.Add(command);
        }

        public IEnumerable<Command> GetAllByPlatformId(int platformId)
        {
            return _context.Commands
                .Where(c => c.PlatformId == platformId)
                .OrderBy(c => c.Platform.Name);
        }

        public Command? GetByIdPlatformId(int id, int platformId)
        {
            return _context.Commands
                .FirstOrDefault(c => c.Id == id &&
                                     c.PlatformId == platformId);
        }
    }
}
