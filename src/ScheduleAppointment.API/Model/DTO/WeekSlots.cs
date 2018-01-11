using System;
using System.Collections.Generic;
using System.Linq;

namespace ScheduleAppointment.API.Model.DTO
{
    public class WeekSlots
    {


        public List<DaySlots> ConsecutiveDaysOfWeek { get; private set; }


        public static WeekSlots CreateAllDaysOfWeekWithNoAvailability(DateTime dtMonday)
        {
            var weekWithNoAvailability = new WeekSlots();

            return new WeekSlots()
            {
                ConsecutiveDaysOfWeek = new List<DaySlots>()
                                        {
                                            DaySlots.CreateDayWithNoAvailability(dtMonday),
                                            DaySlots.CreateDayWithNoAvailability(dtMonday.AddDays(1)),
                                            DaySlots.CreateDayWithNoAvailability(dtMonday.AddDays(2)),
                                            DaySlots.CreateDayWithNoAvailability(dtMonday.AddDays(3)),
                                            DaySlots.CreateDayWithNoAvailability(dtMonday.AddDays(4)),
                                            DaySlots.CreateDayWithNoAvailability(dtMonday.AddDays(5)),
                                            DaySlots.CreateDayWithNoAvailability(dtMonday.AddDays(6))
                                        }
            };
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

        public DateTime CurrentDate { get; private set; }

        public List<IntervalSlot> AvailableSlots { get; private set; }


        public static DaySlots CreateDayWithNoAvailability(DateTime dtCurrentDateDay)
        {
            return new DaySlots(dtCurrentDateDay);
        }


        public DaySlots(DateTime dtCurrentDateDay)
        {
            this.CurrentDate = dtCurrentDateDay;
            this.AvailableSlots = new List<IntervalSlot>();
        }


        public DaySlots(DateTime dtCurrentDateDay, List<IntervalSlot> slots)
        {
            this.CurrentDate = dtCurrentDateDay;
            this.AvailableSlots = slots;
        }


        public DaySlots()
        { }
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
