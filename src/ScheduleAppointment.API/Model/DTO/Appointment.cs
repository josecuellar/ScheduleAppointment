using System;

namespace ScheduleAppointment.API.Model.DTO
{
    public class Appointment
    {
        public Guid FacilityId { get; set; }

        public DateTime Start {get;set;}

        public DateTime End { get; set; }

        public Patient Patient { get; set; }

        public string Comments { get; set; }

        public bool IsValid()
        {
            if (this.Patient != null
                && !string.IsNullOrEmpty(this.Patient.Phone)
                && !string.IsNullOrEmpty(this.Patient.Name)
                && this.Start != null && this.End != null)
                return true;

            return false;
        }
    }


    public class Patient
    {
        public string Name { get; set; }

        public string SecondName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }
}
