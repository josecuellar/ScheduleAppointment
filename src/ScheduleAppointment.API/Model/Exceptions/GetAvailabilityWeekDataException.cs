
using System;

namespace ScheduleAppointment.API.Model.Exceptions
{

    public class GetAvailabilityWeekDataException : Exception
    {
        public string FriendlyMessage { get; private set; }

        public GetAvailabilityWeekDataException()
        {
        }

        public GetAvailabilityWeekDataException(string friendlyMessage, Exception inner)
            : base(inner.Message, inner)
        {
            this.FriendlyMessage = friendlyMessage;
        }
    }

}
