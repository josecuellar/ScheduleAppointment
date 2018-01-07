using System;

namespace ScheduleAppointment.API.Services
{
    public interface ILoggerService
    {
        void Log(Exception location);
    }
}
