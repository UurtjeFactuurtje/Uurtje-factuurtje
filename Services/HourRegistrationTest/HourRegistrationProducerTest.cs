using NUnit.Framework;
using RabbitMQ.Client;
using NSubstitute;
using HourRegistrationAPI.MessageProducer;
using RabbitMQ.Client.Framing.Impl;
using NSubstitute.ExceptionExtensions;
using System;
using NSubstitute.ReceivedExtensions;

namespace HourRegistrationTest
{
    /// <summary>
    /// Tests for the hour registration producer
    /// </summary>
    [TestFixture]
    public class HourRegistrationProducerTest
    {
        IHourRegistrationProducer _producer;
        IConnectionFactory _mockFactory;
        IConnection _mockConnection;

        [OneTimeSetUp]
        public void FixtureSetup()
        {
            Console.WriteLine("Blablabla");
        }

        [SetUp]
        public void Setup()
        {
            _producer = new HourRegistrationProducer();
            _mockFactory = Substitute.For<IConnectionFactory>();
            _mockConnection = Substitute.For<IConnection>();
        }

        /// <summary>
        /// Tests if get connection is true after connection is created 
        /// </summary>
        [Test]
        public void GetConnectionReturnsTrueIfConnectionSuccessfull()
        {
            //Arrange
            _mockFactory.CreateConnection().Returns(_mockConnection);

            //Act + Assert
            Assert.IsTrue(_producer.GetConnection(_mockFactory, 1));
        }

        /// <summary>
        /// Tests if GetConnection tries the specified amount of times before deciding it is not going to work. 
        /// </summary>
        [TestCase (3)]
        public void GetConnectionTriesExpectedAmountOfTimes(int nrOfTries)
        {
            //Arrange
            _mockFactory.CreateConnection().Returns(x => { throw new Exception(); });

            //Act
            var connectionSuccess = _producer.GetConnection(_mockFactory, nrOfTries);

            //Assert
            _mockFactory.Received(nrOfTries).CreateConnection();
            Assert.IsFalse(connectionSuccess);
        }

        /// <summary>
        /// Tests is DataIsValid returns true if the data is valid
        /// </summary>
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void DataIsValidTrueForValidData(int indexOfValidDataList)
        { 
        }

        /// <summary>
        /// Tests is DataIsValid returns false if the data is invalid
        /// </summary>
        [TestCase (0)]
        [TestCase (1)]
        [TestCase (2)]
        [TestCase (3)]
        [TestCase (4)]
        public void DataIsValidFalseForInvalidData(int indexOfInvalidDataList)
        {
        }

        /// <summary>
        /// Tests if production of message returns true if the message was produced
        /// </summary>
        [Test]
        public void ProduceHourRegistrationMessageReturnsTrueForSuccessfullAttempt()
        { }

        /// <summary>
        /// Tests if production of message returns false if the message was not produced
        /// </summary>
        [Test]
        public void ProduceHourRegistrationMessageReturnsFalseForUnsuccessfullAttempt()
        { }
    }
}