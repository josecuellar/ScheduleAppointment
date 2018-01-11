using System;
using System.Collections.Generic;

namespace ScheduleAppointment.API.Model.DTO
{
    public class WeekSlots
    {


        public List<DaySlots> ConsecutiveDaysOfWeek { get; private set; }


        public static WeekSlots CreateAllDaysOfWeekWithNoAvailability()
        {
            var weekWithNoAvailability = new WeekSlots();

            weekWithNoAvailability.ConsecutiveDaysOfWeek = new List<DaySlots>()
            {
                DaySlots.CreateDayWithNoAvailability(),
                DaySlots.CreateDayWithNoAvailability(),
                DaySlots.CreateDayWithNoAvailability(),
                DaySlots.CreateDayWithNoAvailability(),
                DaySlots.CreateDayWithNoAvailability(),
                DaySlots.CreateDayWithNoAvailability(),
                DaySlots.CreateDayWithNoAvailability()
            };

            return weekWithNoAvailability;
        }


        public WeekSlots()
        {
            this.ConsecutiveDaysOfWeek = new List<DaySlots>();
        }


        public WeekSlots(List<DaySlots> daySlots)
        {
            this.ConsecutiveDaysOfWeek = daySlots;
        }


        public void AddDayToWeek(DaySlots daySlots)
        {
            this.ConsecutiveDaysOfWeek.Add(daySlots);
        }
    }


    public class DaySlots
    {


        public List<IntervalSlot> AvailableSlots { get; private set; }


        public static DaySlots CreateDayWithNoAvailability()
        {
            return new DaySlots();
        }


        public DaySlots()
        {
            this.AvailableSlots = new List<IntervalSlot>();
        }


        public DaySlots(List<IntervalSlot> slots)
        {
            this.AvailableSlots = slots;
        }
    }


    public class IntervalSlot
    {
        public TimeSpan Start { get; private set; }

        public TimeSpan End { get; private set; }


        public IntervalSlot(DateTime start, int slotDurationMinutes)
        {
            this.Start = start.TimeOfDay;
            this.End = new DateTime(start.Year, start.Month, start.Day, start.Hour, start.Minute, 0)
                            .AddMinutes(slotDurationMinutes)
                            .TimeOfDay;
        }
    }
}
