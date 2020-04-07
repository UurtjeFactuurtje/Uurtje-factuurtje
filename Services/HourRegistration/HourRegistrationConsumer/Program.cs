using HourRegistrationAPI.Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace HourRegistrationConsumer
{
    class Program
    {
        public static void Main()
        {

            var factory = new ConnectionFactory() { HostName = "rabbitmq" };
            factory.AutomaticRecoveryEnabled = true;

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "IncomingHourRegistrationMessages",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        HourRegistrationModel retrievedModel = GetModelFromBody(body);
                        Console.WriteLine(retrievedModel.Description);
                    };

                    channel.BasicConsume(queue: "IncomingHourRegistrationMessages",
                                         autoAck: true,
                                         consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }

            HourRegistrationModel GetModelFromBody(byte[] body)
            {
                BinaryFormatter bf = new BinaryFormatter();
                object receivedMessage;
                using (MemoryStream ms = new MemoryStream(body))
                {
                    receivedMessage = bf.Deserialize(ms);
                }

                return (HourRegistrationModel)receivedMessage;


            }
        }
    }
}