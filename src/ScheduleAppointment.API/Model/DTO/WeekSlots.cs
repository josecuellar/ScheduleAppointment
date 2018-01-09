using System;
using System.Collections.Generic;

namespace ScheduleAppointment.API.Model.DTO
{
    public class WeekSlots
    {
        public List<Day> ConsecutiveDaysOfWeek;
    }

    public class Day
    {
        public List<DateTime> AvailableTimes { get; set; }
    }
}
