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

        private AvailabilityWeek _weekData { get; set; }


        public WeekSlotsFactory(ILoggerProvider loggerProvider)
        {
            _loggerProvider = loggerProvider;
        }


        public WeekSlots From(AvailabilityWeek weekData)
        {
            try
            {
                _weekData = weekData;

                Condition.Requires(_weekData.SlotDurationMinutes, "slotDurationMinutes")
                    .IsGreaterThan(0)
                    .IsLessThan(120);

                var weekSlots = new WeekSlots();

                foreach (var day in Enum.GetValues(typeof(DayOfWeek))
                                  .OfType<DayOfWeek>()
                                  .OrderBy(x => ((int)x + 6) % 7) //Monday first (Sunday is default order)
                                  .ToList())
                {
                    var dayOfWeek = GetSpecificDataDay(day.ToString());

                    SetCurrentDayOfWeek(day, dayOfWeek);

                    var daySlots = SplitDayInSlots(dayOfWeek);

                    weekSlots.AddDayToWeek(daySlots);
                }

                return weekSlots;

            }
            catch (Exception err)
            {
                _loggerProvider.Log(err);
                return WeekSlots.CreateAllDaysOfWeekWithNoAvailability(_weekData.DayOfMonday);
            }
        }


        private void SetCurrentDayOfWeek(DayOfWeek day, DayOfWeekInfo dayOfWeekInfo)
        {
            if (day == DayOfWeek.Monday)
                dayOfWeekInfo.CurrentDate = _weekData.DayOfMonday;
            else
                dayOfWeekInfo.CurrentDate = _weekData.DayOfMonday.AddDays(((int)day + 6) % 7);
        }


        private DayOfWeekInfo GetSpecificDataDay(string propName)
        {
            var dayOfWeekReflected = (_weekData.GetType().GetProperty(propName).GetValue(_weekData, null));

            if (dayOfWeekReflected == null)
                return new DayOfWeekInfo();

            return (DayOfWeekInfo)dayOfWeekReflected;
        }


        private DaySlots SplitDayInSlots(DayOfWeekInfo dayOfWeekInfo)
        {
            try
            {
                ThrowExceptionIsNotValidDataDay(dayOfWeekInfo);


                var availableSlots = new List<IntervalSlot>();

                var workStartAt = GetDateTimeFrom(dayOfWeekInfo, dayOfWeekInfo.WorkPeriod.StartHour, 0);
                var workEndsAt = GetDateTimeFrom(dayOfWeekInfo, dayOfWeekInfo.WorkPeriod.EndHour, 0);

                var lunchStartAt = GetDateTimeFrom(dayOfWeekInfo, dayOfWeekInfo.WorkPeriod.LunchStartHour, 0);
                var lunchEndsAt = GetDateTimeFrom(dayOfWeekInfo, dayOfWeekInfo.WorkPeriod.LunchEndHour, 0);

                while (workStartAt != workEndsAt)
                {
                    int minutes = +_weekData.SlotDurationMinutes;

                    var slotToAdd = GetDateTimeFrom(dayOfWeekInfo, workStartAt.Hour, workStartAt.Minute);

                    if (IsAvailableSlot(dayOfWeekInfo, slotToAdd, lunchStartAt, lunchEndsAt))
                        availableSlots.Add(new IntervalSlot(slotToAdd, _weekData.SlotDurationMinutes));

                    workStartAt = workStartAt.AddMinutes(minutes);
                }

                return new DaySlots(dayOfWeekInfo.CurrentDate, availableSlots);
            }
            catch (Exception err)
            {
                _loggerProvider.Log(err);
                return DaySlots.CreateDayWithNoAvailability(dayOfWeekInfo.CurrentDate);
            }
        }


        private bool IsAvailableSlot(DayOfWeekInfo dayOfWeekInfo, DateTime slotToAd,  DateTime lunchStartAt, DateTime lunchEndsAt)
        {
            if (IsCurrentSlotBetweenLunchTime(slotToAd, lunchStartAt, lunchEndsAt))
                return false;

            if (IsCurrentSlotBusy(dayOfWeekInfo, slotToAd))
                return false;

            return true;
        }


        private void ThrowExceptionIsNotValidDataDay(DayOfWeekInfo day)
        {
            Condition.Requires(day, "dayOfWeek")
                .IsNotNull();

            Condition.Requires(day.WorkPeriod, "workPeriod")
                .IsNotNull()
                .Evaluate(x => x.EndHour > x.StartHour);
        }


        private bool IsCurrentSlotBetweenLunchTime(DateTime availableSlot, DateTime lunchStart, DateTime lunchEnd)
        {
            if (availableSlot >= lunchStart && availableSlot < lunchEnd)
                return true;

            return false;
        }


        private bool IsCurrentSlotBusy(DayOfWeekInfo dayOfWeekInfo, DateTime availableSlot)
        {
            if (dayOfWeekInfo.BusySlots == null)
                return false;

            return (dayOfWeekInfo.BusySlots.Exists(x => availableSlot == GetDateTimeFrom(dayOfWeekInfo, x.Start.Hour, x.Start.Minute)));
        }


        private DateTime GetDateTimeFrom(DayOfWeekInfo dayOfWeekInfo, int hour, int minute)
        {
            return new DateTime(
                dayOfWeekInfo.CurrentDate.Year,
                dayOfWeekInfo.CurrentDate.Month,
                dayOfWeekInfo.CurrentDate.Day,
                hour, minute, 0);
        }
    }
}
