using ScheduleAppointment.API.Model.DTO;
using System.Collections.Generic;

namespace ScheduleAppointment.API.Factories.Impl
{
    public class WeekSlotsFactory : IFactory<AvailabilityWeek, WeekSlots>
    {
        public WeekSlots From(AvailabilityWeek inputObject)
        {
            return new WeekSlots() { ConsecutiveDaysOfWeek = new List<Day>() { new Day(), new Day(), new Day(), new Day(), new Day(), new Day(), new Day() } };
        }
    }
}
