using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using ScheduleAppointment.API.Model.DTO;
using ScheduleAppointment.API.Providers;
using ScheduleAppointment.API.Services.Impl;
using ScheduleAppointment.API.Settings;
using System;
using System.Threading.Tasks;

namespace ScheduleAppointment.API.Tests.Unit
{
    [TestFixture(Category = "Unit Tests")]
    public class APIAvailabilityWeekServiceShould
    {

        private Mock<IHttpClientProvider> _httpClientProviderMock;

        private Mock<IOptions<APIGetAvailabilityWeekSettings>> _settings;

        private APIAvailabilityWeekService _service;

        private const string URL_CLIENT = "https://teststub.net/api/";
        private const string REQUEST_TEMPLATE = "availability/GetWeeklyAvailability/{0}";
        private const string USER_NAME = "user";
        private const string SECRET_PASSWORD = "secret";


        [SetUp]
        public void SetUp()
        {
            _httpClientProviderMock = new Mock<IHttpClientProvider>();

            _httpClientProviderMock.Setup(m => m.CreateClient(It.IsAny<Uri>())).Returns(_httpClientProviderMock.Object);
            _httpClientProviderMock.Setup(m => m.WithBasicAuthenticator(It.IsAny<string>(), It.IsAny<string>())).Returns(_httpClientProviderMock.Object);
            _httpClientProviderMock.Setup(m => m.WithRequest(It.IsAny<string>())).Returns(_httpClientProviderMock.Object);
            _httpClientProviderMock.Setup(m => m.GetAsync<AvailabilityWeek>()).ReturnsAsync(It.IsAny<AvailabilityWeek>());

            var appSettings = new APIGetAvailabilityWeekSettings()
            {
                UrlClient = URL_CLIENT,
                RequestTemplate = REQUEST_TEMPLATE,
                UserName = USER_NAME,
                Password = SECRET_PASSWORD
            };

            _settings = new Mock<IOptions<APIGetAvailabilityWeekSettings>>();

            _settings
                .Setup(m => m.Value)
                .Returns(appSettings);

            _service = new APIAvailabilityWeekService(_httpClientProviderMock.Object, _settings.Object);
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
            _httpClientProviderMock.Verify(m => m.CreateClient(new Uri(URL_CLIENT)), Times.Once);
            _httpClientProviderMock.Verify(m => m.WithBasicAuthenticator(USER_NAME, SECRET_PASSWORD), Times.Once);
            _httpClientProviderMock.Verify(m => m.WithRequest(string.Format(REQUEST_TEMPLATE, parameter.ToString("yyyyMMdd"))), Times.Once);
            _httpClientProviderMock.Verify(m => m.GetAsync<AvailabilityWeek>(), Times.Once);
        }
    }
}
