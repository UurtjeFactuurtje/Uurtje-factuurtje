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
            HourRegistrationModel model = new HourRegistrationModel();
            model.CompanyId = Guid.NewGuid().ToString();
            model.Description = "blablabla";
            model.EmployeeId = Guid.NewGuid().ToString();
            model.EndTime = DateTime.Parse("2020-04-05T11:50Z");
            model.StartTime = DateTime.Parse("2020-04-05T07:22Z");
            model.ProjectId = Guid.NewGuid().ToString();

            ISession cassandraSession = GetCassandraSession();

            var preparedStatement = cassandraSession.Prepare("INSERT INTO uurtjefactuurtje.hours (id, company_id, project_id, employee_id, start_date, start_time, end_date, end_time, description) VALUES (uuid(), uuid(), uuid(), uuid(), ?, ?, ?, ?, ?)");

            WriteToDatabase(model, cassandraSession, preparedStatement);

            //var factory = new ConnectionFactory() { HostName = "rabbitmq" };
            //factory.AutomaticRecoveryEnabled = true;

            //IConnection connection = GetRabbitMqConnection(factory); 

            //using (connection)
            //{
            //    using (var channel = connection.CreateModel())
            //    {
            //        channel.QueueDeclare(queue: "IncomingHourRegistrationMessages",
            //                             durable: false,
            //                             exclusive: false,
            //                             autoDelete: false,
            //                             arguments: null);

            //        var consumer = new EventingBasicConsumer(channel);
            //        consumer.Received += (model, ea) =>
            //        {
            //            var body = ea.Body;
            //            var message = Encoding.UTF8.GetString(body);
            //            HourRegistrationModel retrievedModel = GetModelFromBody(body);
            //            WriteToDatabase(retrievedModel);
            //            Console.WriteLine(retrievedModel.Description);
            //        };

            //        channel.BasicConsume(queue: "IncomingHourRegistrationMessages",
            //                             autoAck: true,
            //                             consumer: consumer);

            //        Console.WriteLine(" Press [enter] to exit.");
            //        Console.ReadLine();
            //    }
            //}
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
                string startDateBeforeConv = model.StartTime.ToString("yyyy-dd-mm");
                string endDateBeforeConv = model.StartTime.ToString("yyyy-dd-mm");
                string startTimeBeforeConv = model.StartTime.ToString("hh:mm:ss");
                string endTimeBeforeConv = model.EndTime.ToString("hh:mm:ss");
                LocalDate startDate = LocalDate.Parse(startDateBeforeConv);
                LocalDate endDate = LocalDate.Parse(endDateBeforeConv);
                LocalTime startTime = LocalTime.Parse(startTimeBeforeConv);
                LocalTime endTime = LocalTime.Parse(endTimeBeforeConv);
                //var statement = preparedStatement.Bind(model.CompanyId, model.ProjectId, model.EmployeeId, model.StartTime, model.EndTime, model.Description);
                var statement = preparedStatement.Bind(startDate, startTime, endDate, endTime, model.Description);
                session.Execute(statement);
                //session.Execute("INSERT INTO uurtjefactuurtje.hours (id, company_id, project_id, employee_id, start_time, end_time, description) " +
                //                        $"VALUES {model.CompanyId}, {model.ProjectId}, {model.EmployeeId}, {model.StartTime}, {model.EndTime}, {model.Description}");
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

        static ISession GetCassandraSession()
        {
            bool success = false;
            ISession session = null; 

            while (!success)
            {
                try
                {
                    Cluster cluster = Cluster.Builder()
                        .AddContactPoint("cassandra")
                        .Build();

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