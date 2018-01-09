using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ScheduleAppointment.API.Providers.Impl
{
    public class ConsoleLoggerProvider : ILoggerProvider
    {
        public async Task Log(Exception exception)
        {
            await Task.Run(() => Debug.Write(exception));
        }
    }
}
