using System.ComponentModel.DataAnnotations;

namespace PlatformService.Api.Models
{
	public class Platform
	{
		[Key]
		[Required]
		public int Id { get; set; }

		[Required]
		public required string Name { get; set; }

		[Required]
		public required string Publisher { get; set; }

		[Required]
		public string? Cost { get; set; }
	}
}
