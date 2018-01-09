using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ScheduleAppointment.API.Controllers;
using ScheduleAppointment.API.Providers;
using ScheduleAppointment.API.Services;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace ScheduleAppointment.API.Tests.Unit.Controllers
{
    [TestFixture(Category = "Unit Tests")]
    public class GetAvailabilityWeekControllerShould 
    {


        private Mock<ILoggerProvider> _loggerProviderMock;
        private Mock<IAvailabilityWeekService> _availabilityWeekServiceMock;
        private GetAvailabilityWeekController _controller;


        [SetUp]
        public void SetUp()
        {
            _loggerProviderMock = new Mock<ILoggerProvider>();
            _availabilityWeekServiceMock = new Mock<IAvailabilityWeekService>();

            _controller = new GetAvailabilityWeekController(_loggerProviderMock.Object, _availabilityWeekServiceMock.Object);
        }


        [Test]
        public async Task Catch_and_log_exception_given_empty_start_date_of_week()
        {
            // Act
            var result = await _controller.List(string.Empty);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            _loggerProviderMock.Verify(m => m.Log(It.IsAny<ArgumentException>()), Times.Once);
        }


        [Test]
        public async Task Catch_and_log_exception_given_null_start_date_of_week()
        {
            // Act
            var result = await _controller.List(null);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            _loggerProviderMock.Verify(m => m.Log(It.IsAny<ArgumentNullException>()), Times.Once);
        }


        [TestCase("20180417")]
        [TestCase("20181118")]
        public async Task Catch_and_log_exception_given_start_date_of_week_is_not_monday(string startDate)
        {
            // Act
            var result = await _controller.List(startDate);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            _loggerProviderMock.Verify(m => m.Log(It.IsAny<ArgumentException>()), Times.Once);
        }


        [TestCase("20180312")]
        [TestCase("20181015")]
        public async Task ReturnOK_and_verify_calling_correctly_to_service_for_get_availability_of_slots_week(string startDate)
        {
            // Arrange
            DateTime.TryParseExact(startDate, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dayOfStartWeekParsed);

            // Act
            var result = await _controller.List(startDate);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            _availabilityWeekServiceMock.Verify(m => m.GetAvailabilitySlots(dayOfStartWeekParsed), Times.Once);
        }
    }
}
