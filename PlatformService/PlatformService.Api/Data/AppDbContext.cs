using Microsoft.EntityFrameworkCore;
using PlatformService.Api.Models;

namespace PlatformService.Api.Data
{
	public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
	{
		public DbSet<Platform> Platforms { get; set; }
	}
}
