using ScheduleAppointment.API.Model.DTO;
using System;
using System.Threading.Tasks;

namespace ScheduleAppointment.API.Services
{
    public interface IAvailabilityWeekService
    {
        Task<AvailabilityWeek> GetAvailability(DateTime dayOfStartWeek);
    }
}
