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
    public class HourRegistrationProducer : IHourRegistrationProducer
    {
        static ConnectionFactory _factory;
        static IConnection _connection;

        public HourRegistrationProducer()
        {
            _factory = new ConnectionFactory() { HostName = "rabbitmq" };
        }

        public bool ProduceHourRegistrationMessage(HourRegistrationModel registeredHours)
        {
            if (!DataIsValid(registeredHours))
            { return false; }

            if (!GetConnection(_factory, 30))
            { return false; }

            if (!SendMessage(registeredHours))
            { return false; }

            return true;
        }

        public bool GetConnection(IConnectionFactory factory, int nrOfTries)
        {
            bool connectionSuccess = false;
            while (!connectionSuccess && nrOfTries > 0)
            {
                try
                {
                    _connection = factory.CreateConnection();
                    connectionSuccess = true;
                }
                catch (Exception e)
                {
                    connectionSuccess = false;
                    Console.WriteLine($"No connection could be created, the following exception was caught: {e.Message}");
                    Thread.Sleep(1000);
                    nrOfTries = nrOfTries - 1;
                }
            }

            return connectionSuccess;
        }

        private bool SendMessage(HourRegistrationModel registeredHours)
        {
            bool messageWasWritten = false;

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
                    messageWasWritten = true;
                }
            }

            catch (Exception e)
            {
                Console.WriteLine($"Exception was caught when sending a message to the queue: {e.Message}");
                messageWasWritten = false;
            }

            return messageWasWritten;
        }

        public bool DataIsValid(HourRegistrationModel registeredHours)
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

