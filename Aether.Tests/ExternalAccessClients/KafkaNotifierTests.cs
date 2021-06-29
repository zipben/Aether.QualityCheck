using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Aether.TestUtils.BaseClasses;
using APILogger.Interfaces;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using Aether.Models.Configuration;
using Aether.Models.Kafka;
using Aether.ExternalAccessClients;

namespace Aether.Tests.ServiceLayerTests
{
    [TestClass]
    public class KafkaNotifierTests : UnitTestBase
    {
        private const string TEST_SCHEMA = "JsonSchemaTests/test-raw.schema.json";
        private const string TEST_INVALID_SCHEMA = "JsonSchemaTests/test-invalid-raw.schema.json";

        private KafkaNotifier _target;
        private IOptions<KafkaSettings> _kafkaSettings = CreateSettings();
        private Mock<IProducer<string, string>> _mockKafkaProducer;
        private Message<string, string> _capturedMessage;
        private string _topic = Guid.NewGuid().ToString();

        private class TestKafkaMessage : BaseKafkaMessage { }

        [TestInitialize]
        public void Init()
        {
            base.CreateMocks();
            _mockKafkaProducer = new Mock<IProducer<string, string>>();

            _target = new KafkaNotifier(_mockApiLogger.Object, _kafkaSettings, _mockKafkaProducer.Object);
        }

        private void SetupMocks(PersistenceStatus? desiredStatus, bool kafkaThrowsException = false)
        {
            var deliveryResult = new DeliveryResult<string, string>
            {
                Status = desiredStatus ?? PersistenceStatus.NotPersisted
            };

            if (kafkaThrowsException)
            {
                _mockKafkaProducer.Setup(x => x.ProduceAsync(It.IsAny<string>(), It.IsAny<Message<string, string>>(), It.IsAny<CancellationToken>()))
                                  .ThrowsAsync(new Exception("This didn't work"));
            }
            else 
            {
                _mockKafkaProducer.Setup(x => x.ProduceAsync(It.IsAny<string>(), It.IsAny<Message<string, string>>(), It.IsAny<CancellationToken>()))
                                  .Callback((string topic, Message<string, string> msg, CancellationToken token) => _capturedMessage = msg)
                                  .ReturnsAsync(() => desiredStatus.HasValue ? deliveryResult : null);
            }
        }

        public static IEnumerable<object[]> ConstructorTest_ArgumentNullExceptionData()
        {
            yield return new object[] { null, null, null };
            yield return new object[] { null, CreateSettings(), new Mock<IProducer<string, string>>().Object };
            yield return new object[] { new Mock<IApiLogger>().Object, null, new Mock<IProducer<string, string>>().Object };
            yield return new object[] { new Mock<IApiLogger>().Object, CreateSettings(), null };

        }
        [TestMethod]
        [DynamicData(nameof(ConstructorTest_ArgumentNullExceptionData), DynamicDataSourceType.Method)]
        public void ConstructorTest_ArgumentNullException(IApiLogger apiLogger, IOptions<KafkaSettings> kafkaSettings, IProducer<string, string> kafkaProducer)
        {
            if (apiLogger != null) apiLogger = _mockApiLogger.Object;
            Assert.ThrowsException<ArgumentNullException>(() => new KafkaNotifier(apiLogger, kafkaSettings, kafkaProducer));
        }

        public static IEnumerable<object[]> SendAsyncTest_ArgumentNullExceptionData()
        {
            var testString = Guid.NewGuid().ToString();

            yield return new object[] { null, null };
            yield return new object[] { CreateTestMessage(), null };
            yield return new object[] { null, testString };

            var testKafkaMessage = CreateTestMessage();
            testKafkaMessage.Id = null;
            yield return new object[] { testKafkaMessage, testString };

            testKafkaMessage = CreateTestMessage();
            testKafkaMessage.DataSchema = null;
            yield return new object[] { testKafkaMessage, testString };

            testKafkaMessage = CreateTestMessage();
            testKafkaMessage.DataContentType = null;
            yield return new object[] { testKafkaMessage, testString };

            testKafkaMessage = CreateTestMessage();
            testKafkaMessage.Source = null;
            yield return new object[] { testKafkaMessage, testString };

            testKafkaMessage = CreateTestMessage();
            testKafkaMessage.SpecVersion = null;
            yield return new object[] { testKafkaMessage, testString };

            testKafkaMessage = CreateTestMessage();
            testKafkaMessage.Subject = null;
            yield return new object[] { testKafkaMessage, testString };
        }
        [TestMethod]
        [DynamicData(nameof(SendAsyncTest_ArgumentNullExceptionData), DynamicDataSourceType.Method)]
        public async Task SendAsyncTest_ArgumentNullException(BaseKafkaMessage messageContent, string schemaPath)
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _target.SendAsync(messageContent, schemaPath, _topic));
        }

        public static IEnumerable<object[]> SendAsyncTest_ArgumentExceptionData()
        {
            var testString = Guid.NewGuid().ToString();

            yield return new object[] { CreateTestMessage(), string.Empty };

            var testKafkaMessage = CreateTestMessage();
            testKafkaMessage.Id = string.Empty;
            yield return new object[] { testKafkaMessage, testString };

            testKafkaMessage = CreateTestMessage();
            testKafkaMessage.DataSchema = string.Empty;
            yield return new object[] { testKafkaMessage, testString };

            testKafkaMessage = CreateTestMessage();
            testKafkaMessage.DataContentType = string.Empty;
            yield return new object[] { testKafkaMessage, testString };

            testKafkaMessage = CreateTestMessage();
            testKafkaMessage.Source = string.Empty;
            yield return new object[] { testKafkaMessage, testString };

            testKafkaMessage = CreateTestMessage();
            testKafkaMessage.SpecVersion = string.Empty;
            yield return new object[] { testKafkaMessage, testString };

            testKafkaMessage = CreateTestMessage();
            testKafkaMessage.Subject = string.Empty;
            yield return new object[] { testKafkaMessage, testString };
        }
        [TestMethod]
        [DynamicData(nameof(SendAsyncTest_ArgumentExceptionData), DynamicDataSourceType.Method)]
        public async Task SendAsyncTest_ArgumentException(BaseKafkaMessage messageContent, string schemaPath)
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => _target.SendAsync(messageContent, schemaPath, _topic));
        }

        [TestMethod]
        public async Task SendAsyncTest_BlankTopicSkipsSend()
        {
            var kafkaSettings = CreateSettings();
            _topic = string.Empty;

            _target = new KafkaNotifier(_mockApiLogger.Object, kafkaSettings, _mockKafkaProducer.Object);

            SetupMocks(null);
            var response = await _target.SendAsync(CreateTestMessage(), TEST_SCHEMA, _topic);

            Assert.AreEqual(false, response.Successful);

            _mockKafkaProducer.Verify(x => x.ProduceAsync(It.IsAny<string>(), It.IsAny<Message<string, string>>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [TestMethod]
        public async Task SendAsyncTest_InvalidSchema()
        {
            _target = new KafkaNotifier(_mockApiLogger.Object, CreateSettings(), _mockKafkaProducer.Object);

            SetupMocks(PersistenceStatus.Persisted);
            var response = await _target.SendAsync(CreateTestMessage(), TEST_INVALID_SCHEMA, _topic);

            Assert.AreEqual(false, response.Successful);

            _mockKafkaProducer.Verify(x => x.ProduceAsync(It.IsAny<string>(), It.IsAny<Message<string, string>>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [TestMethod]
        public async Task SendAsyncTest_KafkaException()
        {
            _target = new KafkaNotifier(_mockApiLogger.Object, CreateSettings(), _mockKafkaProducer.Object);

            SetupMocks(PersistenceStatus.Persisted, true);
            var response = await _target.SendAsync(CreateTestMessage(), TEST_SCHEMA, _topic);

            Assert.AreEqual(false, response.Successful);

            _mockKafkaProducer.Verify(x => x.ProduceAsync(_topic, It.IsAny<Message<string, string>>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        [DataRow(null, false)]
        [DataRow(PersistenceStatus.PossiblyPersisted, false)]
        [DataRow(PersistenceStatus.NotPersisted, false)]
        [DataRow(PersistenceStatus.Persisted, true)]
        public async Task SendAsyncTest(PersistenceStatus? persistenceStatus, bool expectedSuccess)
        {
            var testMessage = CreateTestMessage();

            SetupMocks(persistenceStatus);
            var response = await _target.SendAsync(testMessage, TEST_SCHEMA, _topic);

            Assert.AreEqual(expectedSuccess, response.Successful);

            _mockKafkaProducer.Verify(x => x.ProduceAsync(_topic, It.IsAny<Message<string, string>>(), default), Times.Once);

            Assert.AreEqual(testMessage.Id, _capturedMessage.Key);
            Assert.AreEqual(JsonConvert.SerializeObject(testMessage), _capturedMessage.Value);
        }

        private static IOptions<KafkaSettings> CreateSettings() => Options.Create(new KafkaSettings
        {
            HostName = Guid.NewGuid().ToString(),
            Password = Guid.NewGuid().ToString(),
            UserName = Guid.NewGuid().ToString()
        });

        private static TestKafkaMessage CreateTestMessage() => new TestKafkaMessage
        {
            DataContentType = Guid.NewGuid().ToString(),
            DataSchema = Guid.NewGuid().ToString(),
            Id = Guid.NewGuid().ToString(),
            Source = Guid.NewGuid().ToString(),
            SpecVersion = Guid.NewGuid().ToString(),
            Subject = Guid.NewGuid().ToString(),
            Time = DateTime.UtcNow
        };
    }
}
