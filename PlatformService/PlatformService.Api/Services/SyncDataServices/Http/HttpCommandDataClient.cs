using PlatformService.Api.Dtos;
using System.Text;
using System.Text.Json;

namespace PlatformService.Api.Services.SyncDataServices.Http
{
	public class HttpCommandDataClient(HttpClient _httpClient, IConfiguration _configuration) : ICommandDataClient
	{
		public async Task SendPlatformToCommand(PlatformReadDto platform)
		{
			var httpContent = new StringContent(
				JsonSerializer.Serialize(platform),
				Encoding.UTF8,
				"application/json");

			var response = await _httpClient.PostAsync($"{_configuration["CommandService"]}", httpContent);

			if (response.IsSuccessStatusCode)
			{
				Console.WriteLine("--> Sync POST to CommandService was OK!");
			}
			else
			{
				Console.WriteLine("--> Sync POST to CommandService was NOT OK!");
			}
		}
	}
}
