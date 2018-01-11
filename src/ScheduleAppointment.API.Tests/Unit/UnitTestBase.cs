using ScheduleAppointment.API.Model.DTO;
using System;

namespace ScheduleAppointment.API.Tests.Unit
{
    public class UnitTestBase
    {
        public const string URL_CLIENT = "https://test.draliacloud.net/api/";
        public const string REQUEST_TEMPLATE = "availability/GetWeeklyAvailability/{0}";
        public const string REQUEST_TAKESLOT = "availability/TakeSlot";
        public const string USER = "techuser";
        public const string PSW = "secretpassWord";


        public Appointment VALID_SLOT_STUB = new Appointment()
        {
            Start = DateTime.Now,
            End = DateTime.Now.AddMinutes(60),
            Patient = new Patient()
            {
                Name = "Jose",
                Phone = "646 85 57 47",
            }
        };
    }
}
