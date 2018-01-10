namespace ScheduleAppointment.API.Model.DTO
{
    public class AvailabilityWeek
    {
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
}
