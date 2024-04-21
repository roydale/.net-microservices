using PlatformService.Api.Models;

namespace PlatformService.Api.Data
{
	public class PlatformRepository(AppDbContext _context) : IPlatformRepository
	{
		public void Create(Platform platform)
		{
			ArgumentNullException.ThrowIfNull(platform);
			_context.Platforms.Add(platform);
		}

		public IEnumerable<Platform> GetAll()
		{
			return [.. _context.Platforms];
		}

		public Platform? GetById(int id)
		{
			return _context.Platforms.FirstOrDefault(p => p.Id == id!);
		}

		public bool SaveChanges()
		{
			return (_context.SaveChanges() >= 0);
		}
	}
}
