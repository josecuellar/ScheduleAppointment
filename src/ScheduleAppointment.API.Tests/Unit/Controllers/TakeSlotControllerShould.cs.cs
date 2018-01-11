using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ScheduleAppointment.API.Controllers;
using ScheduleAppointment.API.Model.DTO;
using ScheduleAppointment.API.Providers;
using ScheduleAppointment.API.Services;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace ScheduleAppointment.API.Tests.Unit.Controllers
{
    [TestFixture(Category = "Unit Tests")]
    public class TakeSlotControllerShould : UnitTestBase
    {

        private Mock<ILoggerProvider> _loggerProviderMock;
        private Mock<IAvailabilityWeekService> _availabilityWeekServiceMock;

        private TakeSlotController _controller;


        [SetUp]
        public void SetUp()
        {
            _loggerProviderMock = new Mock<ILoggerProvider>();
            _availabilityWeekServiceMock = new Mock<IAvailabilityWeekService>();

            _controller = new TakeSlotController(_loggerProviderMock.Object, _availabilityWeekServiceMock.Object);
        }


        [Test]
        public async Task Catch_and_log_exception_given_null_parameter()
        {
            // Act
            var result = await _controller.Post(null);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            _loggerProviderMock.Verify(m => m.Log(It.IsAny<ArgumentNullException>()), Times.Once);
        }


        [Test]
        public async Task Catch_and_log_exception_given_parameter_with_null_or_empty_mandatory_fields()
        {
            // Act
            var result = await _controller.Post(new Appointment());

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            _loggerProviderMock.Verify(m => m.Log(It.IsAny<ArgumentException>()), Times.Once);
        }


        [Test]
        public async Task Return_ok_given_valid_parameter_slot()
        {
            // Act
            var result = await _controller.Post(VALID_SLOT_STUB);

            // Assert
            Assert.IsInstanceOf<CreatedResult>(result);
            _availabilityWeekServiceMock.Verify(m => m.TakeAppointment(VALID_SLOT_STUB), Times.Once);
        }
    }
}
