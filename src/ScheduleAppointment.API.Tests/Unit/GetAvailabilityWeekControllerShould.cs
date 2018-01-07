using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ScheduleAppointment.API.Controllers;
using ScheduleAppointment.API.Services;
using System;
using System.Threading.Tasks;

namespace ScheduleAppointment.API.Tests.Unit
{
    [TestFixture(Category = "Unit Tests")]
    public class GetAvailabilityWeekControllerShould 
    {


        private Mock<ILoggerService> _loggerServiceMock;
        private GetAvailabilityWeekController _controller;


        [SetUp]
        public void SetUp()
        {
            _loggerServiceMock = new Mock<ILoggerService>();
            _controller = new GetAvailabilityWeekController(_loggerServiceMock.Object);
        }


        [Test]
        public async Task Catch_and_log_exception_given_empty_start_date_of_week()
        {
            // Act
            var result = await _controller.List(string.Empty);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            _loggerServiceMock.Verify(m => m.Log(It.IsAny<ArgumentException>()), Times.Once);
        }


        [Test]
        public async Task Catch_and_log_exception_given_null_start_date_of_week()
        {
            // Act
            var result = await _controller.List(null);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            _loggerServiceMock.Verify(m => m.Log(It.IsAny<ArgumentNullException>()), Times.Once);
        }


        [TestCase("20180417")]
        [TestCase("20181118")]
        public async Task Catch_and_log_exception_given_start_date_of_week_is_not_monday(string startDate)
        {
            // Act
            var result = await _controller.List(startDate);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            _loggerServiceMock.Verify(m => m.Log(It.IsAny<ArgumentException>()), Times.Once);
        }


        [TestCase("20160417")]
        [TestCase("19000117")]
        public async Task Catch_and_log_exception_given_start_date_of_week_is_less_than_today(string startDate)
        {
            // Act
            var result = await _controller.List(startDate);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            _loggerServiceMock.Verify(m => m.Log(It.IsAny<ArgumentOutOfRangeException>()), Times.Once);
        }
    }
}
