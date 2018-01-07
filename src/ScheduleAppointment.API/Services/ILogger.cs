using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleAppointment.API.Services
{
    public interface ILogger
    {
        Task Log(Exception location);
    }
}
