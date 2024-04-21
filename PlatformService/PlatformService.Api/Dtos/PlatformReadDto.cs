namespace PlatformService.Api.Dtos
{
	public class PlatformReadDto
	{
		public int Id { get; set; }

		public required string Name { get; set; }

		public required string Publisher { get; set; }

		public string? Cost { get; set; }
	}
}
