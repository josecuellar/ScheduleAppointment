using ScheduleAppointment.API.Model.DTO;
using System;
using System.Threading.Tasks;

namespace ScheduleAppointment.API.Services
{
    public interface IAvailabilityWeekService
    {
        Task<AvailabilityWeek> GetAvailabilityWeekData(DateTime dayOfStartWeek);

        Task<WeekSlots> GetAvailabilitySlots(DateTime dayOfStartWeek);
    }
}
