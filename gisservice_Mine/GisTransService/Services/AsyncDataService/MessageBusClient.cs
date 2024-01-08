using System;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using GisCommonService.Params;

namespace GisTransService.Services.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBusClient(IConfiguration configuration)
        {
            _configuration = configuration;
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"])
            };
            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.ExchangeDeclare(exchange: "SaveTrigger", type: ExchangeType.Fanout);
                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
                Console.WriteLine("--> Connection to MessageBus");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Cound not connect to Message Bus: {ex.Message}");
            }
        }

        public void SendRequestToSaveGis(AddrSaveParam param)
        {
            var message = JsonSerializer.Serialize(param);
            if (_connection.IsOpen)
            {
                Console.WriteLine($"--> RabbitMQ Connection Open, sending message... SEQNO:{param.SEQNO.ToString()}");
                SendMessage(message);
            }
            else
            {
                Console.WriteLine("--> RabbitMQ Connection Closed, not sending");
            }
        }

        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "SaveTrigger",
            routingKey: "",
            basicProperties: null,
            body: body);

            Console.WriteLine($"--> We have send {message}");
        }

        public void Dispose()
        {
            Console.WriteLine("MessageBus Disposed");
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("--> RabbitMQ Connection Shutdown");
        }
    }
}