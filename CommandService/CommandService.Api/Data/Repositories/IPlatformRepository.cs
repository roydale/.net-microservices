using CommandService.Api.Models;

namespace CommandService.Api.Data.Repositories
{
	public interface IPlatformRepository : IBaseRepository
	{
		IEnumerable<Platform> GetAll();

		void Create(Platform platform);

		bool IsExist(int id);

		bool IsExternalPlatformExist(int externalId);
	}
}
