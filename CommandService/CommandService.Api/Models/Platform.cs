using System.ComponentModel.DataAnnotations;

namespace CommandService.Api.Models
{
	public class Platform
	{
		[Key]
		[Required]
		public int Id { get; set; }

		[Required]
		public int ExternalId { get; set; }

		[Required]
		public required string Name { get; set; }

		public ICollection<Command> Commands { get; set; } = [];
	}
}
