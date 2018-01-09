using System.Collections.Generic;

namespace ScheduleAppointment.API.Model.DTO
{
    public class DayOfWeek
    {
        public WorkPeriod WorkPeriod { get; set; }

        public List<BusySlot> BusySlots { get; set; }
    }
}
