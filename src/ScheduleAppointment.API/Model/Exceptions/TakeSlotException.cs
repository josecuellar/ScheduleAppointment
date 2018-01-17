
using System;

namespace ScheduleAppointment.API.Model.Exceptions
{

    public class TakeAppointmentException : Exception
    {
        public string FriendlyMessage { get; private set; }

        public TakeAppointmentException()
        {
        }

        public TakeAppointmentException(string friendlyMessage, Exception inner)
            : base(inner.Message, inner)
        {
            this.FriendlyMessage = friendlyMessage;
        }
    }

}
