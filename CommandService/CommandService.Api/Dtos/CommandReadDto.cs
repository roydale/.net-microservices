using System.ComponentModel.DataAnnotations;

namespace CommandService.Api.Dtos
{
	public class CommandReadDto
	{
		public int Id { get; set; }

		public required string HowTo { get; set; }

		public required string CommandLine { get; set; }

		public int PlatformId { get; set; }
	}
}
