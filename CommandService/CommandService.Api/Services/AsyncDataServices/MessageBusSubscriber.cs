﻿using CommandService.Api.EventProcessing;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace CommandService.Api.Services.AsyncDataServices
{
	public class MessageBusSubscriber : BackgroundService
	{
		private readonly IConfiguration _configuration;
		private readonly IEventProcessor _eventProcessor;
		private IConnection? _connection;
		private IModel? _channel;
		private string? _queueName;

		public MessageBusSubscriber(IConfiguration configuration,
									IEventProcessor eventProcessor)
		{
			_configuration = configuration;
			_eventProcessor = eventProcessor;
			InitializeRabbitMQConnection();
		}

		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			stoppingToken.ThrowIfCancellationRequested();

			var consumer = new EventingBasicConsumer(_channel);
			consumer.Received += (ModuleHandle, basicDeliverEventArgs) =>
			{
				Console.WriteLine("---> Event Received");

				var body = basicDeliverEventArgs.Body;
				var notificationMessage = Encoding.UTF8.GetString(body.ToArray());

				_eventProcessor.ProcessEvent(notificationMessage);
			};

			_channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
			return Task.CompletedTask;
		}

		public override void Dispose()
		{
			if (_channel!.IsOpen)
			{
				_channel.Close();
				_connection!.Close();
				Console.WriteLine("---> Message Bus Disposed");
			}
			base.Dispose();
		}

		private void InitializeRabbitMQConnection()
		{
			var factory = new ConnectionFactory()
			{
				HostName = _configuration["RabbitMQHost"],
				Port = int.Parse(_configuration["RabbitMQPort"]!)
			};

			_connection = factory.CreateConnection();
			_channel = _connection.CreateModel();
			_channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

			_queueName = _channel.QueueDeclare().QueueName;
			_channel.QueueBind(queue: _queueName, exchange: "trigger", routingKey: "");
			Console.WriteLine("---> Listening on the Message Bus");
		}

		private void RabbitMQConnectionShutdowm(object? sender, ShutdownEventArgs e)
		{
			Console.WriteLine("---> RabbitMQ Connection Shutdown");
		}
	}
}
