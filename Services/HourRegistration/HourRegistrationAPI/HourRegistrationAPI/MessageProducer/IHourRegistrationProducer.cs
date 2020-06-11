using HourRegistrationAPI.Model;
using RabbitMQ.Client;

namespace HourRegistrationAPI.MessageProducer
{
    public interface IHourRegistrationProducer
    {
        bool ProduceHourRegistrationMessage(HourRegistrationModel registeredHours);
        bool GetConnection(IConnectionFactory factory, int nrOfTries);
        bool DataIsValid(HourRegistrationModel registeredHours);
    }
}