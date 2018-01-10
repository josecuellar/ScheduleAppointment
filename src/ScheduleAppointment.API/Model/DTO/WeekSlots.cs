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


        public List<TimeSpan> AvailableSlots { get; private set; }


        public static DaySlots CreateDayWithNoAvailability()
        {
            return new DaySlots();
        }


        public DaySlots()
        {
            this.AvailableSlots = new List<TimeSpan>();
        }


        public DaySlots(List<TimeSpan> slots)
        {
            this.AvailableSlots = slots;
        }
    }
}
