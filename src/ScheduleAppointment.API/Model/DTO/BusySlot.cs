using System;
using System.Collections.Generic;

namespace ScheduleAppointment.API.Model.DTO
{
    public class BusySlot
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}
