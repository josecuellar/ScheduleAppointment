namespace ScheduleAppointment.API.Model.DTO
{
    public class AvailabilityWeek
    {
        public Facility Facility { get; set; }

        public int SlotDurationMinutes { get; set; }

        public DayOfWeek Monday { get; set; }

        public DayOfWeek Tuesday { get; set; }

        public DayOfWeek Wednesday { get; set; }

        public DayOfWeek Thursday { get; set; }

        public DayOfWeek Friday { get; set; }

        public DayOfWeek Saturday { get; set; }

        public DayOfWeek Sunday { get; set; }
    }
}
