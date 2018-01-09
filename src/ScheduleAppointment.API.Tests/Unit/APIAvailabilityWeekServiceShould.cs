using Moq;
using NUnit.Framework;
using ScheduleAppointment.API.Model.DTO;
using ScheduleAppointment.API.Providers;
using ScheduleAppointment.API.Services.Impl;
using System;
using System.Threading.Tasks;

namespace ScheduleAppointment.API.Tests.Unit
{
    [TestFixture(Category = "Unit Tests")]
    public class APIAvailabilityWeekServiceShould
    {

        private Mock<IHttpClientProvider> _httpClientProviderMock;

        private Mock<ILoggerProvider> _loggerProvider;

        private APIAvailabilityWeekService _service;

        [SetUp]
        public void SetUp()
        {
            _httpClientProviderMock = new Mock<IHttpClientProvider>();

            _httpClientProviderMock
                .Setup(m => m.CreateClient(It.IsAny<Uri>()))
                .Returns(_httpClientProviderMock.Object);

            _httpClientProviderMock
                .Setup(m => m.WithBasicAuthenticator(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(_httpClientProviderMock.Object);

            _httpClientProviderMock
                .Setup(m => m.GetAsync<AvailabilityWeek>(It.IsAny<string>()))
                .ReturnsAsync(It.IsAny<AvailabilityWeek>());


            _loggerProvider = new Mock<ILoggerProvider>();

            _service = new APIAvailabilityWeekService(_httpClientProviderMock.Object, _loggerProvider.Object);
        }


        [TestCase(2018, 3, 12)]
        [TestCase(2018, 10, 15)]
        public async Task Catch_and_log_exception_when_error_occurred_calling_to_api_method_get_availability_week(int year, int month, int day)
        {
            // Arrange
            DateTime parameter = new DateTime(year, month, day);
            var exception = new Exception();

            _httpClientProviderMock
                .Setup(m => m.GetAsync<AvailabilityWeek>(It.IsAny<string>()))
                .ThrowsAsync(exception);

            // Act
            try
            {
                var result = await _service.GetAvailability(parameter);
                Assert.Fail();
            }
            catch (Exception)
            { }

            // Assert
            _loggerProvider.Verify(m => m.Log(exception), Times.Once);
        }


        [TestCase(2018, 3, 12)]
        [TestCase(2018, 10, 15)]
        public async Task Verify_correctly_request_to_api_method_when_get_availability_week(int year, int month, int day)
        {
            // Arrange
            DateTime parameter = new DateTime(year, month, day);

            // Act
            var result = await _service.GetAvailability(parameter);

            // Assert
            _httpClientProviderMock.Verify(m => m.CreateClient(new Uri("https://test.draliacloud.net/api/")), Times.Once);
            _httpClientProviderMock.Verify(m => m.WithBasicAuthenticator("techuser", "secretpassWord"), Times.Once);
            _httpClientProviderMock.Verify(m => m.GetAsync<AvailabilityWeek>(string.Format("availability/GetWeeklyAvailability/{0}", parameter.ToString("yyyyMMdd"))), Times.Once);
        }
    }
}
