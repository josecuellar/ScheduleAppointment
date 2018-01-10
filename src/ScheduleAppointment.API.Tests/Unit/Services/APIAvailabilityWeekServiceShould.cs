using Moq;
using NUnit.Framework;
using ScheduleAppointment.API.Factories;
using ScheduleAppointment.API.Model.DTO;
using ScheduleAppointment.API.Providers;
using ScheduleAppointment.API.Services.Impl;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScheduleAppointment.API.Tests.Unit.Services
{
    [TestFixture(Category = "Unit Tests")]
    public class APIAvailabilityWeekServiceShould : UnitTestBase
    {

        private Mock<IHttpClientProvider> _httpClientProviderMock;

        private Mock<ILoggerProvider> _loggerProvider;

        private Mock<IFactory<AvailabilityWeek, WeekSlots>> _weekSlotsFactory;

        private const string validJSON = "{'Facility':{'Name':'Las Palmeras','Address':'Plaza de la independencia 36, 38006 Santa Cruz de Tenerife'},'SlotDurationMinutes':10,'Monday':{'WorkPeriod':{'StartHour':9,'EndHour':17,'LunchStartHour':13,'LunchEndHour':14}},'Wednesday':{'WorkPeriod':{'StartHour':9,'EndHour':17,'LunchStartHour':13,'LunchEndHour':14}},'Friday':{'WorkPeriod':{'StartHour':8,'EndHour':16,'LunchStartHour':13,'LunchEndHour':14}}}";

        private APIAvailabilityWeekService _service;


        [SetUp]
        public void SetUp()
        {
            _httpClientProviderMock = new Mock<IHttpClientProvider>();

            _httpClientProviderMock
                .Setup(m => m.CreateClient(URL_CLIENT))
                .Returns(_httpClientProviderMock.Object);

            _httpClientProviderMock
                .Setup(m => m.WithBasicAuthenticator(USER, PSW))
                .Returns(_httpClientProviderMock.Object);

            _httpClientProviderMock
                .Setup(m => m.GetStringAsync(It.IsAny<string>()))
                .ReturnsAsync(It.IsAny<string>());

            _loggerProvider = new Mock<ILoggerProvider>();

            _weekSlotsFactory = new Mock<IFactory<AvailabilityWeek, WeekSlots>>();

            _weekSlotsFactory
                .Setup(m => m.From(It.IsAny<AvailabilityWeek>()))
                .Returns(WeekSlots.CreateAllDaysOfWeekWithNoAvailability());

            _service = new APIAvailabilityWeekService(
                                _httpClientProviderMock.Object, 
                                _loggerProvider.Object,
                                _weekSlotsFactory.Object);
        }


        [TestCase(2018, 3, 12)]
        [TestCase(2018, 10, 15)]
        public async Task Catch_and_log_exception_when_error_occurred_calling_to_get_availability_week_api_method(int year, int month, int day)
        {
            // Arrange
            DateTime parameter = new DateTime(year, month, day);
            var exception = new Exception();

            _httpClientProviderMock
                .Setup(m => m.GetStringAsync(It.IsAny<string>()))
                .ThrowsAsync(exception);

            // Act
            try
            {
                var result = await _service.GetAvailabilityWeekData(parameter);
                Assert.Fail();
            }
            catch (Exception)
            { }

            // Assert
            _loggerProvider.Verify(m => m.Log(exception), Times.Once);
        }


        [Test]
        public async Task Return_correct_instance_of_available_slots_when_calling_service_with_valid_parameters()
        {
            // Act
            var result = await _service.GetAvailabilitySlots(It.IsAny<DateTime>());

            // Assert
            Assert.IsInstanceOf<WeekSlots>(result);
            Assert.AreEqual(result.ConsecutiveDaysOfWeek.Count, 7);
        }


        [TestCase(2018, 3, 12)]
        [TestCase(2018, 10, 15)]
        public async Task Catch_and_log_exception_when_error_occurred_calling_to_get_availability_slots_week_api_method(int year, int month, int day)
        {
            // Arrange
            DateTime parameter = new DateTime(year, month, day);
            var exception = new Exception();

            _weekSlotsFactory
                .Setup(m => m.From(It.IsAny<AvailabilityWeek>()))
                .Throws(exception);

            // Act
            try
            {
                var result = await _service.GetAvailabilitySlots(parameter);
                Assert.Fail();
            }
            catch (Exception)
            { }

            // Assert
            _loggerProvider.Verify(m => m.Log(exception), Times.Once);
        }


        [TestCase(2018, 3, 12)]
        [TestCase(2018, 10, 15)]
        public async Task Verify_map_to_available_slots_of_week_when_calling_service_with_valid_parameters(int year, int month, int day)
        {
            // Arrange
            DateTime parameter = new DateTime(year, month, day);

            // Act
            var result = await _service.GetAvailabilitySlots(parameter);

            // Assert
            _weekSlotsFactory.Verify(m => m.From(It.IsAny<AvailabilityWeek>()), Times.Once);
        }


        [TestCase(2018, 3, 12)]
        [TestCase(2018, 10, 15)]
        public async Task Verify_correct_configuration_request_for_get_availability_week_api_method(int year, int month, int day)
        {
            // Arrange
            DateTime parameter = new DateTime(year, month, day);
            string request = string.Format(REQUEST_TEMPLATE, parameter.ToString("yyyyMMdd"));

            // Act
            var result = await _service.GetAvailabilityWeekData(parameter);

            // Assert
            _httpClientProviderMock.Verify(m => m.CreateClient(URL_CLIENT), Times.Once);
            _httpClientProviderMock.Verify(m => m.WithBasicAuthenticator(USER, PSW), Times.Once);
            _httpClientProviderMock.Verify(m => m.GetStringAsync(request), Times.Once);
        }


        [Test]
        public async Task Returns_correct_availability_week_dto_when_calling_service_with_valid_parameters()
        {
            // Arrange
            _httpClientProviderMock
                .Setup(m => m.GetStringAsync(It.IsAny<string>()))
                .ReturnsAsync(validJSON);

            // Act
            var result = await _service.GetAvailabilityWeekData(It.IsAny<DateTime>());

            // Assert
            Assert.IsInstanceOf<AvailabilityWeek>(result);

            Assert.AreEqual(result.Facility.Name, "Las Palmeras");
            Assert.AreEqual(result.Facility.Address, "Plaza de la independencia 36, 38006 Santa Cruz de Tenerife");

            Assert.AreEqual(result.SlotDurationMinutes, 10);

            Assert.IsTrue(result.Monday.WorkPeriod.StartHour == 9
                            && result.Wednesday.WorkPeriod.StartHour == 9
                            && result.Friday.WorkPeriod.StartHour == 8);

            Assert.IsTrue(result.Monday.WorkPeriod.EndHour == 17
                && result.Wednesday.WorkPeriod.EndHour == 17
                && result.Friday.WorkPeriod.EndHour == 16);

            Assert.IsTrue(result.Monday.WorkPeriod.LunchStartHour == 13
                && result.Wednesday.WorkPeriod.LunchStartHour == 13
                && result.Friday.WorkPeriod.LunchStartHour == 13);

            Assert.IsTrue(result.Monday.WorkPeriod.LunchEndHour == 14
                && result.Wednesday.WorkPeriod.LunchEndHour == 14
                && result.Friday.WorkPeriod.LunchEndHour == 14);
        }
    }
}
