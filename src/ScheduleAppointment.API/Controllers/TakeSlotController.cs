using Conditions;
using Microsoft.AspNetCore.Mvc;
using ScheduleAppointment.API.Model.DTO;
using ScheduleAppointment.API.Providers;
using ScheduleAppointment.API.Services;
using System;
using System.Threading.Tasks;

namespace ScheduleAppointment.API.Controllers
{
    public class TakeSlotController : Controller
    {

        private ILoggerProvider _loggerProvider;
        private IAvailabilityWeekService _availabilityWeekService;


        public TakeSlotController(
            ILoggerProvider loggerProvider,
            IAvailabilityWeekService availabilityWeekService)
        {
            _loggerProvider = loggerProvider;
            _availabilityWeekService = availabilityWeekService;
        }


        [HttpPost]
        [Route("api/availability/takeslot")]
        public async Task<IActionResult> Post([FromBody] Appointment appointment)
        {
            try
            {
                Condition.Requires(appointment)
                    .IsNotNull("Appointment is required");

                Condition.Requires(appointment)
                    .Evaluate(x => x.IsValid(), "Appointment is invalid (startdate, enddate, patient phone and name are mandatory)");

                await _availabilityWeekService.TakeAppointment(appointment);

                return Created("api/AvailableWeekSlots", appointment);
            }
            catch (Exception err)
            {
                await _loggerProvider.Log(err);
                return BadRequest(err);
            }
        }
    }
}
