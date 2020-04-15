using HourRegistrationAPI.Model;
using RabbitMQ.Client;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace HourRegistrationAPI.MessageProducer
{
    public class HourRegistrationProducer
    {
        static ConnectionFactory _factory;
        static IConnection _connection;

        public HourRegistrationProducer()
        { 
            _factory = new ConnectionFactory() { HostName = "rabbitmq" };
            _connection = null;
        }

        bool GetConnection(ConnectionFactory factory)
        {
            bool connectionSuccess = false;
            while (!connectionSuccess)
            {
                try
                {
                    _connection = factory.CreateConnection();
                    connectionSuccess = true;
                }
                catch (Exception e)
                {
                    connectionSuccess = false;
                }
            }
            return connectionSuccess;
        }

        public bool ProduceHourRegistrationMessage(HourRegistrationModel registeredHours)
        {
            try
            {
                if (!_connection.IsOpen)
                {
                    GetConnection(_factory);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("No connection established yet");
                GetConnection(_factory);
            }

            bool success = true;

            try
            {
                using (_connection)
                using (var channel = _connection.CreateModel())
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

