using Conditions;
using Microsoft.AspNetCore.Mvc;
using ScheduleAppointment.API.Providers;
using ScheduleAppointment.API.Services;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace ScheduleAppointment.API.Controllers
{
    public class GetAvailabilityWeekController : Controller
    {

        private ILoggerProvider _loggerProvider;
        private IAvailabilityWeekService _availabilityWeekService;


        public GetAvailabilityWeekController(
            ILoggerProvider loggerProvider,
            IAvailabilityWeekService availabilityWeekService)
        {
            _loggerProvider = loggerProvider;
            _availabilityWeekService = availabilityWeekService;
        }


        [HttpGet]
        [Route("api/AvailableWeekSlots/{dayOfStartWeek?}")]
        public async Task<IActionResult> List(string dayOfStartWeek)
        {
            try
            {
                Condition.Requires(dayOfStartWeek)
                    .IsNotNullOrEmpty("Day start of weeb is mandatory");

                DateTime.TryParseExact(dayOfStartWeek, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dayOfStartWeekParsed);

                Condition.Requires(dayOfStartWeekParsed)
                    .Evaluate(x => x.DayOfWeek == DayOfWeek.Monday, "Weekly based method: always expect Monday");

                var result = await _availabilityWeekService.GetAvailabilitySlots(dayOfStartWeekParsed);

                return Ok(result);
            }
            catch (Exception err)
            {
                await _loggerProvider.Log(err);
                return BadRequest(err);
            }
        }
    }
}
