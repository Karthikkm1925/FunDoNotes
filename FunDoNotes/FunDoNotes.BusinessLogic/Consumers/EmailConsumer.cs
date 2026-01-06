using FunDoNotes.BusinessLogic.Interfaces;
using FunDoNotes.BusinessLogic.Services;
using FunDoNotes.Model.DTOs.Email;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FunDoNotes.BusinessLogic.Consumers
{
    public class EmailConsumer : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public EmailConsumer(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;

            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(
                queue: "email_queue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (sender, args) =>
            {
                using var scope = _scopeFactory.CreateScope();

                var emailService = scope.ServiceProvider
                    .GetRequiredService<IEmailService>();

                var body = Encoding.UTF8.GetString(args.Body.ToArray());
                var message = JsonSerializer.Deserialize<SendEmailDto>(body);

                if (message != null)
                {
                    emailService.SendEmail(message); 
                }
            };

            _channel.BasicConsume(
                queue: "email_queue",
                autoAck: true,
                consumer: consumer);

            return Task.CompletedTask;
        }
    }

}
