using HourRegistrationAPI.Model;
using RabbitMQ.Client;
using System;
using System.Text;

namespace HourRegistrationAPI.MessageProducer
{
    public class HourRegistrationProducer
    {

        public bool ProduceHourRegistrationMessage(HourRegistrationModel registeredHours)
        {
            bool success = true;

            try
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

            catch (Exception e)
            {
                Console.WriteLine($"Exception was caught when sending a message to the queue: {e.Message}");
                success = false;
            }

            return success;
        }
    }
}

