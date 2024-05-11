using CommandService.Api.Data.Repositories;
using CommandService.Api.Models;
using CommandService.Api.Services.SyncDataServices.Grpc;

namespace CommandService.Api.Data
{
	public static class InitializeDatabase
	{
		public static void PrePopulate(IApplicationBuilder applicationBuilder)
		{
			using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
			var grpcClient = serviceScope.ServiceProvider.GetService<IPlatformDataClient>();
			var platforms = grpcClient!.ReturnAllPlatforms();
			SeedData(serviceScope.ServiceProvider.GetService<IPlatformRepository>()!, platforms);
		}

		private static void SeedData(IPlatformRepository _repository, IEnumerable<Platform> platforms)
		{
			Console.WriteLine("---> Seeding data from Platform database using GRPC...");

			foreach (var platform in platforms)
			{
				if (!_repository.IsExternalPlatformExist(platform.ExternalId))
				{
					_repository.Create(platform);
				}
				_repository.SaveChanges();
			}
		}
	}
}
