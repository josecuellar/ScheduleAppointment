using Conditions;
using Microsoft.AspNetCore.Mvc;
using ScheduleAppointment.API.Services;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace ScheduleAppointment.API.Controllers
{
    public class GetAvailabilityWeekController : Controller
    {

        private ILoggerService _loggerService;


        public GetAvailabilityWeekController(ILoggerService loggerService)
        {
            _loggerService = loggerService;
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


                return Ok();
            }
            catch (Exception err)
            {
                _loggerService.Log(err);
                return BadRequest(err);
            }
        }
    }
}
