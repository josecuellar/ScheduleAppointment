using System;
using System.Collections.Generic;

namespace ScheduleAppointment.API.Model.DTO
{
    public class WorkPeriod
    {
        public int StartHour { get; set; }

        public int EndHour { get; set; }

        public int LunchStartHour { get; set; }

        public int LunchEndHour { get; set; }
    }
}
