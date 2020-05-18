using Cassandra;
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
                catch (Exception)
                {
                    connectionSuccess = false;
                }
            }
            return connectionSuccess;
        }

        public bool ProduceHourRegistrationMessage(HourRegistrationModel registeredHours)
        {
            if (!DataIsValid(registeredHours))
            { return false; }

            bool success = true;

            try
            {
                if (!_connection.IsOpen)
                {
                    GetConnection(_factory);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("No connection established yet");
                GetConnection(_factory);
            }

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
                    Console.WriteLine(message.Description);
                }
            }

            catch (Exception e)
            {
                Console.WriteLine($"Exception was caught when sending a message to the queue: {e.Message}");
                success = false;
            }

            return success;
        }

        private bool DataIsValid(HourRegistrationModel registeredHours)
        {
            bool valid = false;
            try
            {
                LocalDate startDate = LocalDate.Parse($"{registeredHours.StartTime.Year}-{registeredHours.StartTime.Month}-{registeredHours.StartTime.Day}");
                LocalDate endDate = LocalDate.Parse($"{registeredHours.EndTime.Year}-{registeredHours.EndTime.Month}-{registeredHours.EndTime.Day}");
                LocalTime startTime = LocalTime.Parse($"{registeredHours.StartTime.Hour}:{registeredHours.StartTime.Minute}:{registeredHours.StartTime.Second}");
                LocalTime endTime = LocalTime.Parse($"{registeredHours.EndTime.Hour}:{registeredHours.EndTime.Minute}:{registeredHours.EndTime.Second}");
                Guid emp = Guid.Parse(registeredHours.EmployeeId);
                Guid com = Guid.Parse(registeredHours.CompanyId);
                Guid proj = Guid.Parse(registeredHours.ProjectId);

                if (registeredHours.StartTime < registeredHours.EndTime)
                {
                    valid = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"The format was not correct! {e.Message}");
            }
            return valid;
        }


    }
}

