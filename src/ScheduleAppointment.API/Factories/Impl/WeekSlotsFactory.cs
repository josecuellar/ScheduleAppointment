using Conditions;
using ScheduleAppointment.API.Model.DTO;
using ScheduleAppointment.API.Providers;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ScheduleAppointment.API.Factories.Impl
{
    public class WeekSlotsFactory : IFactory<AvailabilityWeek, WeekSlots>
    {


        private ILoggerProvider _loggerProvider;


        public WeekSlotsFactory(ILoggerProvider loggerProvider)
        {
            _loggerProvider = loggerProvider;
        }


        public WeekSlots From(AvailabilityWeek weekData)
        {
            try
            {
                Condition.Requires(weekData.SlotDurationMinutes, "slotDurationMinutes")
                    .IsGreaterThan(0)
                    .IsLessThan(120);

                var weekSlots = new WeekSlots();

                foreach (var day in Enum.GetValues(typeof(DayOfWeek))
                                  .OfType<DayOfWeek>()
                                  .OrderBy(x => ((int)x + 6) % 7) //Monday first (Sunday is default order)
                                  .ToList())
                {
                    var dayOfWeek = GetSpecificDataDay(weekData, day.ToString());

                    var daySlots = CreateAvailableDaySlots(dayOfWeek, weekData.SlotDurationMinutes);

                    weekSlots.AddDayToWeek(daySlots);
                }

                return weekSlots;

            }
            catch (Exception err)
            {
                _loggerProvider.Log(err);
                return WeekSlots.CreateAllDaysOfWeekWithNoAvailability();
            }
        }


        private DayOfWeekInfo GetSpecificDataDay(object objectToReflection, string propName)
        {
            var dayOfWeekReflected = (objectToReflection.GetType().GetProperty(propName).GetValue(objectToReflection, null));

            if (dayOfWeekReflected == null)
                return new DayOfWeekInfo();

            return (DayOfWeekInfo)dayOfWeekReflected;
        }


        private DaySlots CreateAvailableDaySlots(DayOfWeekInfo dayOfWeekInfo, int slotDurationMinutes)
        {
            try
            {
                ThrowExceptionIsNotValidDataDay(dayOfWeekInfo, slotDurationMinutes);


                var availableSlots = new List<IntervalSlot>();

                var workStartAt = CreateDateTimeFromHour(dayOfWeekInfo.WorkPeriod.StartHour);
                var workEndsAt = CreateDateTimeFromHour(dayOfWeekInfo.WorkPeriod.EndHour);

                var lunchStartAt = CreateTimeSpanFromHour(dayOfWeekInfo.WorkPeriod.LunchStartHour);
                var lunchEndsAt = CreateTimeSpanFromHour(dayOfWeekInfo.WorkPeriod.LunchEndHour);

                while (workStartAt != workEndsAt)
                {
                    int minutes = +slotDurationMinutes;
                    workStartAt = workStartAt.AddMinutes(minutes);

                    var slotToAdd = CreateTimeSpanFromHourAndMinute(workStartAt.Hour, workStartAt.Minute);

                    if (IsAvailableSlot(slotToAdd, dayOfWeekInfo, workStartAt, lunchStartAt, lunchEndsAt))
                        availableSlots.Add(new IntervalSlot(slotToAdd, slotDurationMinutes));
                }

                return new DaySlots(availableSlots);
            }
            catch (Exception err)
            {
                _loggerProvider.Log(err);
                return DaySlots.CreateDayWithNoAvailability();
            }
        }


        private bool IsAvailableSlot(
            TimeSpan slotToAd, 
            DayOfWeekInfo day, 
            DateTime workStartAt, 
            TimeSpan lunchStartAt, 
            TimeSpan lunchEndsAt)
        {
            if (IsCurrentSlotBetweenLunchTime(slotToAd, lunchStartAt, lunchEndsAt))
                return false;

            if (IsCurrentSlotBusy(slotToAd, day.BusySlots))
                return false;

            return true;
        }


        private void ThrowExceptionIsNotValidDataDay(
            DayOfWeekInfo day, 
            int slotDurationMinutes)
        {
            Condition.Requires(day, "dayOfWeek")
                .IsNotNull();

            Condition.Requires(day.WorkPeriod, "workPeriod")
                .IsNotNull()
                .Evaluate(x => x.EndHour > x.StartHour);
        }


        private bool IsCurrentSlotBetweenLunchTime(
            TimeSpan availableSlot, 
            TimeSpan lunchStart, 
            TimeSpan lunchEnd)
        {
            if (availableSlot >= lunchStart && availableSlot < lunchEnd)
                return true;

            return false;
        }


        private bool IsCurrentSlotBusy(TimeSpan availableSlot, List<BusySlot> busySlots)
        {
            if (busySlots == null)
                return false;

            return ExistStartSlotInListOfBusySlots(availableSlot, busySlots);
        }


        private bool ExistStartSlotInListOfBusySlots(TimeSpan availableSlot, List<BusySlot> busySlots)
        {
            return (busySlots.Exists(isBetween => availableSlot == CreateTimeSpanFromHourAndMinute(isBetween.Start.Hour, isBetween.Start.Minute)));
        }


        private TimeSpan CreateTimeSpanFromHour(int hour)
        {
            return TimeSpan.FromHours(hour);
        }


        private TimeSpan CreateTimeSpanFromHourAndMinute(int hour, int minute)
        {
            return new TimeSpan(hour, minute, 0);
        }


        private DateTime CreateDateTimeFromHour(int hour)
        {
            return new DateTime(1, 1, 1, hour, 0, 0);
        }
    }
}
