using CommandService.Api.Models;

namespace CommandService.Api.Data.Repositories
{
	public class PlatformRepository(AppDbContext context) : BaseRepository(context), IPlatformRepository
	{
		private readonly AppDbContext _context = context;

		public void Create(Platform platform)
		{
			ArgumentNullException.ThrowIfNull(platform);
			_context.Platforms.Add(platform);
		}

		public IEnumerable<Platform> GetAll()
		{
			return [.. _context.Platforms];
		}

		public bool IsExist(int id)
		{
			return _context.Platforms.Any(p => p.Id == id);
		}

		public bool IsExternalPlatformExist(int externalId)
		{
			return _context.Platforms.Any(p => p.ExternalId == externalId);
		}
	}
}
