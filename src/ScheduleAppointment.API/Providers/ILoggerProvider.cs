using System;
using System.Threading.Tasks;

namespace ScheduleAppointment.API.Providers
{
    public interface ILoggerProvider
    {
        Task Log(Exception location);
    }
}
