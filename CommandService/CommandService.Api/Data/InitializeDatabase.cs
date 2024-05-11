using CommandService.Api.Models;
using CommandService.Api.Services.SyncDataServices.Grpc;
using Microsoft.EntityFrameworkCore;

namespace CommandService.Api.Data
{
	public static class InitializeDatabase
	{
		public static void PrePopulate(IApplicationBuilder applicationBuilder, bool isProduction)
		{
			using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
			var grpcClient = serviceScope.ServiceProvider.GetService<IPlatformDataClient>();
			var platforms = grpcClient!.ReturnAllPlatforms();

			SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>()!, isProduction, platforms);
		}

		private static void SeedData(AppDbContext context, bool isProduction, IEnumerable<Platform> platforms)
		{
			if (isProduction)
			{
				try
				{
					Console.WriteLine("---> Attempting to apply migrations...");
					context.Database.Migrate();
				}
				catch (Exception exception)
				{
					Console.WriteLine("---> Could not run migrations: {0}", exception.Message);
				}
			}

			if (platforms.Any())
			{
				Console.WriteLine("---> Seeding data from Platform database using GRPC...");
				var platformList = platforms
					.Where(platform => !context.Platforms.Any(p => p.ExternalId == platform.ExternalId))
					.Select(platform => platform);
				context.AddRange(platformList);
				context.SaveChanges();
			}
			else
			{
				Console.WriteLine("---> No data from Platform database to add");
			}
		}
	}
}
