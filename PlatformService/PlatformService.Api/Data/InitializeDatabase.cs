using PlatformService.Api.Models;

namespace PlatformService.Api.Data
{
	public static class InitializeDatabase
	{
		public static void PrePopulate(IApplicationBuilder app)
		{
			using var serviceScope = app.ApplicationServices.CreateScope();
			SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>()!);
		}

		private static void SeedData(AppDbContext context)
		{
			if (!context.Platforms.Any())
			{
				Console.WriteLine("Seeding Data...");
				context.Platforms.AddRange(
					new Platform() { Name = ".Net", Publisher = "Microsoft", Cost = "Free" },
					new Platform() { Name = "SQL Server Express", Publisher = "Microsoft", Cost = "Free" },
					new Platform() { Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free" },
					new Platform() { Name = "RavenDB", Publisher = "Hibernating Rhinos", Cost = "Free/Subscription" },
					new Platform() { Name = "Docker", Publisher = "Docker, Inc.", Cost = "Free/Subscription" },
					new Platform() { Name = "Octopus Deploy", Publisher = "Octopus Deploy", Cost = "Subscription" }
				);
				context.SaveChanges();
			}
			else
			{
				Console.WriteLine("Data already exist");
			}
		}
	}
}
