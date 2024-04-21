using PlatformService.Api.Models;

namespace PlatformService.Api.Data
{
	public interface IPlatformRepository
	{
		bool SaveChanges();

		IEnumerable<Platform> GetAll();

		Platform? GetById(int id);

		void Create(Platform platform);
	}
}
