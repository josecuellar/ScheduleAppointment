using Moq;
using NUnit.Framework;
using ScheduleAppointment.API.Model.DTO;
using ScheduleAppointment.API.Providers;
using ScheduleAppointment.API.Providers.Impl;
using ScheduleAppointment.API.Services.Impl;
using System;
using System.Threading.Tasks;

namespace ScheduleAppointment.API.Tests.Unit.Providers
{
    [TestFixture(Category = "Unit Tests")]
    public class HttpClientProviderShould : UnitTestBase
    {

        private Mock<ILoggerProvider> _loggerProvider;

        private HttpClientProvider _provider;


        [SetUp]
        public void SetUp()
        {
            _loggerProvider = new Mock<ILoggerProvider>();

            _provider = new HttpClientProvider(_loggerProvider.Object);
        }


        [TestCase("")]
        [TestCase(null)]
        [TestCase("notvalidurl")]
        public void Catch_and_log_exception_when_createclient_with_invalid_string_url(string url)
        {
            // Act
            try
            {
                var result = _provider.CreateClient(url);
                Assert.Fail();
            }
            catch (Exception)
            { }

            // Assert
            _loggerProvider.Verify(m => m.Log(It.IsAny<Exception>()), Times.Once);
        }


        [TestCase("", "")]
        [TestCase(null, null)]
        [TestCase(null, "")]
        [TestCase("", null)]
        public void Catch_and_log_exception_when_configure_basic_authentication_with_invalid_data_user(string user, string password)
        {
            // Act
            try
            {
                var result = _provider.WithBasicAuthenticator(user, password);
                Assert.Fail();
            }
            catch (Exception)
            { }

            // Assert
            _loggerProvider.Verify(m => m.Log(It.IsAny<Exception>()), Times.Once);
        }


        [TestCase("")]
        [TestCase(null)]
        public async Task Catch_and_log_exception_when_get_availability_week_with_invalid_request(string request)
        {
            // Act
            try
            {
                var result = await _provider.CreateClient(URL_CLIENT)
                                                .WithBasicAuthenticator(USER, PSW)
                                                .GetStringAsync(request);

                Assert.Fail();
            }
            catch (Exception)
            { }

            // Assert
            _loggerProvider.Verify(m => m.Log(It.IsAny<Exception>()), Times.Once);
        }
    }
}
