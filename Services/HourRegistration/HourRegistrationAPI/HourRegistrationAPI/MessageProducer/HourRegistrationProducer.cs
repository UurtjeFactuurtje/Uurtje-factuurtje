using HourRegistrationAPI.Model;
using RabbitMQ.Client;
using System;
using System.Text;

namespace HourRegistrationAPI.MessageProducer
{
    public class HourRegistrationProducer
    {

        public void ProduceHourRegistrationMessage(HourRegistrationModel registeredHours)
        {
            var factory = new ConnectionFactory() { HostName = "rabbitmq" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "IncomingHourRegistrationMessages",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = registeredHours.ToString();
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "IncomingHourRegistrationMessages",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine($"Following message was sent to queue: {message}");
            }
        }
    }
}
