using System;
using System.Diagnostics;

namespace ScheduleAppointment.API.Services.Impl
{
    public class ConsoleLoggerService : ILoggerService
    {
        public void Log(Exception exception)
        {
            Debug.Write(exception);
        }
    }
}
