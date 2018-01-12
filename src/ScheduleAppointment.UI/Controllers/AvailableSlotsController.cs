using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ScheduleAppointment.UI.Controllers
{
    [Route("api/[controller]")]
    public class AvailableSlotsController : Controller
    {
        [HttpGet("[action]")]
        public IEnumerable<AvailableWeekSlots> Week()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new AvailableWeekSlots
            {
                Day1 = DateTime.Now.AddDays(index).ToString("d"),
                Day2 = DateTime.Now.AddDays(index).ToString("d"),
                Day3 = DateTime.Now.AddDays(index).ToString("d"),
                Day4 = DateTime.Now.AddDays(index).ToString("d"),
                Day5 = DateTime.Now.AddDays(index).ToString("d"),
                Day6 = DateTime.Now.AddDays(index).ToString("d"),
                Day7 = DateTime.Now.AddDays(index).ToString("d")
            });
        }

        public class AvailableWeekSlots
        {
            public string Day1 { get; set; }
            public string Day2 { get; set; }
            public string Day3 { get; set; }
            public string Day4 { get; set; }
            public string Day5 { get; set; }
            public string Day6 { get; set; }
            public string Day7 { get; set; }
        }
    }
}
