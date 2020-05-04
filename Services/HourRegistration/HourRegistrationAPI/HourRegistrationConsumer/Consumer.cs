using Cassandra;
using HourRegistrationAPI.Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;

namespace HourRegistrationConsumer
{
    class Consumer
    {
        static public void Main()
        {
           
            var factory = new ConnectionFactory() { HostName = "rabbitmq" };
            factory.AutomaticRecoveryEnabled = true;

            IConnection connection = GetRabbitMqConnection(factory);
            Cluster cluster = Cluster.Builder()
                .AddContactPoint("cassandra")
                .Build();


            using (connection)
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

                        ISession cassandraSession = GetCassandraSession(cluster);
                        var preparedStatement = cassandraSession.Prepare("INSERT INTO uurtjefactuurtje.registered_hours_by_employee (company_id, project_id, employee_id, start_date, start_time, end_date, end_time, description) VALUES (?, ?, ?, ?, ?, ?, ?, ?)");

                        WriteToDatabase(retrievedModel, cassandraSession, preparedStatement);
                        Console.WriteLine(retrievedModel.Description);
                    };

                    channel.BasicConsume(queue: "IncomingHourRegistrationMessages",
                                         autoAck: true,
                                         consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }

        static HourRegistrationModel GetModelFromBody(byte[] body)
        {
            BinaryFormatter bf = new BinaryFormatter();
            object receivedMessage;
            using (MemoryStream ms = new MemoryStream(body))
            {
                receivedMessage = bf.Deserialize(ms);
            }

            return (HourRegistrationModel)receivedMessage;
        }

        static bool WriteToDatabase(HourRegistrationModel model, ISession session, PreparedStatement preparedStatement)
        {
            bool success = true;
            try
            {
                LocalDate startDate = LocalDate.Parse($"{model.StartTime.Year}-{model.StartTime.Month}-{model.StartTime.Day}");
                LocalDate endDate = LocalDate.Parse($"{model.EndTime.Year}-{model.EndTime.Month}-{model.EndTime.Day}");
                LocalTime startTime = LocalTime.Parse($"{model.StartTime.Hour}:{model.StartTime.Minute}:{model.StartTime.Second}");
                LocalTime endTime = LocalTime.Parse($"{model.EndTime.Hour}:{model.EndTime.Minute}:{model.EndTime.Second}");
                Guid emp = Guid.Parse(model.EmployeeId);
                Guid com = Guid.Parse(model.CompanyId);
                Guid proj = Guid.Parse(model.ProjectId);
                var statement = preparedStatement.Bind(com, proj, emp, startDate, startTime, endDate, endTime, model.Description);
                session.ExecuteAsync(statement);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Writing message to database was not successfull, following error was thrown: {e.Message}");
                success = false;
            }
            return success;
        }

        static IConnection GetRabbitMqConnection(ConnectionFactory factory)
        {
            IConnection connection = null;
            bool connectionSuccess = false;
            while (!connectionSuccess)
            {
                try
                {
                    Thread.Sleep(200);
                    connection = factory.CreateConnection();
                    connectionSuccess = true;
                }
                catch (Exception e)
                {
                    connectionSuccess = false;
                }
            }
            return connection;
        }

        static ISession GetCassandraSession(Cluster cluster)
        {
            bool success = false;
            ISession session = null; 

            while (!success)
            {
                try
                {
                    session = cluster.Connect();
                    success = true;
                }
                catch(Exception e)
                {
                    Console.WriteLine($"Couldn't connect to the cluster {e.Message}");
                }
            }

            return session;
        }
    }
}