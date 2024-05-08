using AutoMapper;
using CommandService.Api.Data.Repositories;
using CommandService.Api.Dtos;
using CommandService.Api.Enums;
using CommandService.Api.Models;
using System.Text.Json;

namespace CommandService.Api.EventProcessing
{
	public class EventProcessor(IServiceScopeFactory _scopeFactory, IMapper _mapper) : IEventProcessor
	{
		public void ProcessEvent(string message)
		{
			var eventType = DetermineEventType(message);

			switch (eventType)
			{
				case EventTypes.PlatformPublished:
					AddPlatform(message);
					break;
				default:
					break;
			}
		}

		private EventTypes DetermineEventType(string notificationMessage)
		{
			Console.WriteLine("---> Determining Event");
			var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

			switch (eventType!.Event)
			{
				case "Platform_Published":
					Console.WriteLine("---> Platform Published Event Detected");
					return EventTypes.PlatformPublished;
				default:
					Console.WriteLine("---> Could not determine the event type");
					return EventTypes.Undetermined;
			}
		}

		private void AddPlatform(string platformPublishedMessage)
		{
			using var scope = _scopeFactory.CreateScope();
			var repository = scope.ServiceProvider.GetRequiredService<IPlatformRepository>();
			var platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishedDto>(platformPublishedMessage);
			try
			{
				var platform = _mapper.Map<Platform>(platformPublishedDto);
				if (!repository.IsExternalPlatformExist(platform.ExternalId))
				{
					repository.Create(platform);
					repository.SaveChanges();
					Console.WriteLine("---> Platform added to CommandDb");
				}
				else
				{
					Console.WriteLine("---> Platform already exist...");
				}
			}
			catch (Exception exception)
			{
				Console.WriteLine("---> Could not add Platform to database. {0}", exception.Message);
			}
		}
	}
}
