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
        public static void Main()
        {
            var factory = new ConnectionFactory() { HostName = "rabbitmq" };
            factory.AutomaticRecoveryEnabled = true;

            IConnection connection = GetConnection(factory); 

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
                        //WriteToDatabase(retrievedModel);
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

            //bool WriteToDatabase(HourRegistrationModel model)
            //{
            //    bool success = true;
            //    try
            //    {
            //        var cluster = Cluster.Builder()
            //            .AddContactPoint("127.0.0.1")
            //            .Build();

            //        var session = cluster.Connect();
            //        session.Execute("INSERT INTO hours (id, company_id, project_id, employee_id, start_time, end_time, description) " +
            //                                $"VALUES uuid() {model.CompanyId}, {model.ProjectId}, {model.EmployeeId}, {model.StartTime}, {model.EndTime}, {model.Description}");
            //    }
            //    catch (Exception e)
            //    {
            //        Console.WriteLine($"Writing message to database was not successfull, following error was thrown: {e.Message}");
            //        success = false;
            //    }
            //    return success;
            //}

            IConnection GetConnection(ConnectionFactory factory)
            {
                IConnection connection = null;
                bool connectionSuccess = false;
                while (!connectionSuccess)
                {
                    try
                    {
                        Thread.Sleep(1000);
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
        }
    }
}