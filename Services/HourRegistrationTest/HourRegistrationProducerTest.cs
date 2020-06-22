using NUnit.Framework;
using RabbitMQ.Client;
using NSubstitute;
using HourRegistrationAPI.MessageProducer;
using System;
using NSubstitute.ReceivedExtensions;
using HourRegistrationAPI.Model;
using System.Collections.Generic;

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

        List<HourRegistrationModel> _invalidModelsList = new List<HourRegistrationModel>();
        List<HourRegistrationModel> _validModelsList = new List<HourRegistrationModel>();

        [OneTimeSetUp]
        public void FixtureSetup()
        {
            AddInvalidModels();
            AddValidModels();
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
        public void DataIsValidTrueForValidData(int indexOfValidDataList)
        {
            //Arrange
            HourRegistrationModel model = _validModelsList[indexOfValidDataList];

            //Act + Assert
            Assert.True(_producer.DataIsValid(model));
        }

        /// <summary>
        /// Tests is DataIsValid returns false if the data is invalid
        /// </summary>
        [TestCase (0)]
        [TestCase (1)]
        [TestCase (2)]
        [TestCase (3)]
        [TestCase (4)]
        [TestCase (5)]
        public void DataIsValidFalseForInvalidData(int indexOfInvalidDataList)
        {
            //Arrange
            HourRegistrationModel model = _invalidModelsList[indexOfInvalidDataList];

            //Act + Assert
            Assert.False(_producer.DataIsValid(model));
        }

        /// <summary>
        /// Tests if production of message returns false if the message was not produced
        /// </summary>
        [Test]
        public void ProduceHourRegistrationMessageReturnsFalseForUnsuccessfullAttempt()
        {
            //Arrange + Act + Assert
            Assert.False(_producer.ProduceHourRegistrationMessage(_invalidModelsList[0]));
        }

        public void AddInvalidModels()
        {
            var model = new HourRegistrationModel
            {
                CompanyId = "invalid",
                EmployeeId = Guid.NewGuid().ToString(),
                ProjectId = Guid.NewGuid().ToString(),
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,
                Description = "Valid description"
            };
            var model1 = new HourRegistrationModel
            {
                CompanyId = Guid.NewGuid().ToString(),
                EmployeeId = "invalid",
                ProjectId = Guid.NewGuid().ToString(),
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,
                Description = "Valid description"
            };
            var model2 = new HourRegistrationModel
            {
                CompanyId = Guid.NewGuid().ToString(),
                EmployeeId = Guid.NewGuid().ToString(),
                ProjectId = "invalid",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,
                Description = "Valid description"
            };
            var model3 = new HourRegistrationModel
            {
                CompanyId = Guid.NewGuid().ToString(),
                EmployeeId = Guid.NewGuid().ToString(),
                ProjectId = Guid.NewGuid().ToString(),
                Description = "Valid description"
            };
            var model4 = new HourRegistrationModel
            {
                CompanyId = Guid.NewGuid().ToString(),
                EmployeeId = Guid.NewGuid().ToString(),
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,
                Description = "Valid description"
            };
            var model5 = new HourRegistrationModel
            {
                CompanyId = Guid.NewGuid().ToString(),
                EmployeeId = Guid.NewGuid().ToString(),
                ProjectId = Guid.NewGuid().ToString(),
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddMonths(-1),
                Description = "Valid description"
            };

            _invalidModelsList.Add(model);
            _invalidModelsList.Add(model1);
            _invalidModelsList.Add(model2);
            _invalidModelsList.Add(model3);
            _invalidModelsList.Add(model4);
            _invalidModelsList.Add(model5);
        }

        public void AddValidModels()
        {
            var model = new HourRegistrationModel
            {
                CompanyId = Guid.NewGuid().ToString(),
                EmployeeId = Guid.NewGuid().ToString(),
                ProjectId = Guid.NewGuid().ToString(),
                StartTime = DateTime.Now,
                EndTime = DateTime.Now
            };
            var model1 = new HourRegistrationModel
            {
                CompanyId = Guid.NewGuid().ToString(),
                EmployeeId = Guid.NewGuid().ToString(),
                ProjectId = Guid.NewGuid().ToString(),
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,
                Description = "Valid description"
            };
            _validModelsList.Add(model);
            _validModelsList.Add(model1);
        }
    }
}