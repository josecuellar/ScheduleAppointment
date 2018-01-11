using Moq;
using NUnit.Framework;
using ScheduleAppointment.API.Factories.Impl;
using ScheduleAppointment.API.Model.DTO;
using ScheduleAppointment.API.Providers;
using System;
using System.Collections.Generic;

namespace ScheduleAppointment.API.Tests.Unit.Factories
{
    [TestFixture(Category = "Unit Tests")]
    public class WeekSlotsFactoryShould : UnitTestBase
    {

        private Mock<ILoggerProvider> _loggerProvider;

        private WeekSlotsFactory _factory;


        [SetUp]
        public void SetUp()
        {
            _loggerProvider = new Mock<ILoggerProvider>();

            _factory = new WeekSlotsFactory(_loggerProvider.Object);
        }


        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(121)]
        [TestCase(500)]
        public void Log_exception_and_return_list_days_of_week_with_no_availability_when_invalid_slot_interval(int intervalSlot)
        {
            // Act
            var result = _factory.From(new Model.DTO.AvailabilityWeek() { SlotDurationMinutes = intervalSlot });

            // Assert
            _loggerProvider.Verify(m => m.Log(It.IsAny<Exception>()), Times.Once);
            AssertValidWeekSlotsWithNoAvailability(result);
        }


        [TestCase(20, 20)]
        [TestCase(20, 10)]
        [TestCase(0, 0)]
        [TestCase(-9, -17)]
        [TestCase(9, -17)]
        public void Log_exception_and_return_list_days_of_week_with_no_availability_when_invalid_work_interval_hours(int workStart, int workEnd)
        {
            // Act
            var result = _factory.From(new Model.DTO.AvailabilityWeek() { SlotDurationMinutes = 0 });

            // Assert
            _loggerProvider.Verify(m => m.Log(It.IsAny<Exception>()), Times.Once);
            AssertValidWeekSlotsWithNoAvailability(result);
        }


        [Test]
        public void Not_return_available_start_hour_when_is_busy()
        {
            // Act
            var result = _factory.From(new Model.DTO.AvailabilityWeek()
            {
                SlotDurationMinutes = 60,
                Monday = new DayOfWeekInfo()
                {
                    WorkPeriod = new WorkPeriod()
                    {
                        StartHour  = 10,
                        EndHour = 14
                    },
                    BusySlots = new List<BusySlot>()
                                {
                                    new BusySlot(){ Start = new DateTime(1, 1, 1, 10, 0, 0) },
                                    new BusySlot(){ Start = new DateTime(1, 1, 1, 12, 0, 0) }
                                }
                }
            });

            // Assert
            Assert.IsTrue(GetDaySlotsFromIndex(0, result).Exists(x => x.Start == new TimeSpan(11, 0, 0)));
            Assert.IsFalse(GetDaySlotsFromIndex(0, result).Exists(x => x.Start == new TimeSpan(10, 0, 0)));
            Assert.IsFalse(GetDaySlotsFromIndex(0, result).Exists(x => x.Start == new TimeSpan(12, 0, 0)));
        }


        [Test]
        public void Not_return_available_start_hour_when_is_lunch_time()
        {
            // Act
            var result = _factory.From(new Model.DTO.AvailabilityWeek()
            {
                SlotDurationMinutes = 60,
                Monday = new DayOfWeekInfo()
                {
                    WorkPeriod = new WorkPeriod()
                    {
                        StartHour = 10,
                        EndHour = 17,
                        LunchStartHour = 14,
                        LunchEndHour = 16
                    }
                }
            });

            // Assert
            Assert.IsTrue(GetDaySlotsFromIndex(0, result).Exists(x => x.Start == new TimeSpan(16, 0, 0)));
            Assert.IsFalse(GetDaySlotsFromIndex(0, result).Exists(x => x.Start == new TimeSpan(14, 0, 0)));
            Assert.IsFalse(GetDaySlotsFromIndex(0, result).Exists(x => x.Start == new TimeSpan(15, 0, 0)));
        }


        private void AssertValidWeekSlotsWithNoAvailability(WeekSlots result)
        {
            Assert.AreEqual(result.ConsecutiveDaysOfWeek.Count, 7);

            for (var dayCounter = 0; dayCounter <= 6; dayCounter++)
                Assert.AreEqual(GetDaySlotsFromIndex(dayCounter, result).Count, 0);
        }


        private List<IntervalSlot> GetDaySlotsFromIndex(int indexToGet, WeekSlots weekSlots)
        {
            return weekSlots.ConsecutiveDaysOfWeek[indexToGet].AvailableSlots;
        }
    }
}
