using HourRegistrationAPI.Model;
using RabbitMQ.Client;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

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

                    HourRegistrationModel message = registeredHours;
                    IFormatter formatter = new BinaryFormatter();

                    MemoryStream memoryStream = new MemoryStream();

                    formatter.Serialize(memoryStream, message);
                    var body = memoryStream.ToArray();

                    channel.BasicPublish(exchange: "",
                                         routingKey: "IncomingHourRegistrationMessages",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine("Following message was sent to queue:");
                    Console.WriteLine(message.ToString());
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

