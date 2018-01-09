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

        private ILoggerProvider _loggerService;
        private IAvailabilityWeekService _availabilityWeekService;


        public GetAvailabilityWeekController(
            ILoggerProvider loggerService,
            IAvailabilityWeekService availabilityWeekService)
        {
            _loggerService = loggerService;
            _availabilityWeekService = availabilityWeekService;
        }


        [HttpGet]
        [Route("api/[controller]/{dayOfStartWeek?}")]
        public async Task<IActionResult> List(string dayOfStartWeek)
        {
            try
            {
                Condition.Requires(dayOfStartWeek)
                    .IsNotNullOrEmpty("Day start of weeb is mandatory");

                DateTime.TryParseExact(dayOfStartWeek, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dayOfStartWeekParsed);

                Condition.Requires(dayOfStartWeekParsed)
                    .IsGreaterThan(DateTime.Today, "Availability must be more bigger than today")
                    .Evaluate(x => x.DayOfWeek == DayOfWeek.Monday, "Weekly based method: always expect Monday");

                var result = await _availabilityWeekService.GetAvailability(dayOfStartWeekParsed);

                return Ok(result);
            }
            catch (Exception err)
            {
                await _loggerService.Log(err);
                return BadRequest(err);
            }
        }
    }
}
