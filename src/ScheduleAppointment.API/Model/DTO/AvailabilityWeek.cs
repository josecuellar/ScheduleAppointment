using System;
using System.Collections.Generic;

namespace ScheduleAppointment.API.Model.DTO
{

    public class AvailabilityWeek
    {
        public DateTime DayOfMonday { get; set; }

        public Facility Facility { get; set; }

        public int SlotDurationMinutes { get; set; }

        public DayOfWeekInfo Monday { get; set; }

        public DayOfWeekInfo Tuesday { get; set; }

        public DayOfWeekInfo Wednesday { get; set; }

        public DayOfWeekInfo Thursday { get; set; }

        public DayOfWeekInfo Friday { get; set; }

        public DayOfWeekInfo Saturday { get; set; }

        public DayOfWeekInfo Sunday { get; set; }
    }


    public class BusySlot
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }


    public class DayOfWeekInfo
    {
        public DateTime CurrentDate { get; set; }

        public WorkPeriod WorkPeriod { get; set; }

        public List<BusySlot> BusySlots { get; set; }
    }


    public class Facility
    {
        public string Name { get; set; }

        public string Address { get; set; }
    }


    public class WorkPeriod
    {
        public int StartHour { get; set; }

        public int EndHour { get; set; }

        public int LunchStartHour { get; set; }

        public int LunchEndHour { get; set; }
    }
}
