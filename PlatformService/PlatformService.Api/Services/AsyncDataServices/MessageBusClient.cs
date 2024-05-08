using PlatformService.Api.Dtos;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace PlatformService.Api.Services.AsyncDataServices
{
	public class MessageBusClient : IMessageBusClient
	{
		private readonly IConfiguration _configuration;
		private IConnection? _connection;
		private IModel? _channel;

		public MessageBusClient(IConfiguration configuration)
		{
			_configuration = configuration;
			InitializeRabbitMQConnection();
		}

		public void PublishNewPlatform(PlatformPublishedDto platformPublisedDto)
		{
			var message = JsonSerializer.Serialize(platformPublisedDto);

			if (_connection != null && _connection.IsOpen)
			{
				Console.WriteLine("---> RabbitMQ Connection open, sending message...");
				SendMessage(message);
			}
			else
			{
				Console.WriteLine("---> RabbitMQ Connection is closed, not sending");
				// TODO: Try to implement a retry mechanism
			}
		}

		public void Dispose()
		{
			if (_channel!.IsOpen)
			{
				_channel.Close();
				_connection!.Close();
				Console.WriteLine("---> Message Bus Disposed");
			}
		}

		private void InitializeRabbitMQConnection()
		{
			var factory = new ConnectionFactory()
			{
				HostName = _configuration["RabbitMQHost"],
				Port = int.Parse(_configuration["RabbitMQPort"]!)
			};

			try
			{
				_connection = factory.CreateConnection();
				_channel = _connection.CreateModel();
				_channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

				_connection.ConnectionShutdown += RabbitMQConnectionShutdowm;
				Console.WriteLine("---> Connected to Message Bus");
			}
			catch (Exception exception)
			{
				Console.WriteLine("---> Could not connect to the Message Bus: {0}", exception.Message);
			}
		}

		private void RabbitMQConnectionShutdowm(object? sender, ShutdownEventArgs e)
		{
			Console.WriteLine("---> RabbitMQ Connection Shutdown");
		}

		private void SendMessage(string message)
		{
			var body = Encoding.UTF8.GetBytes(message);
			_channel.BasicPublish("trigger", string.Empty, null, body);
			Console.WriteLine("---> We have sent {0}", message);
		}
	}
}
