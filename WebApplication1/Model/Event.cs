using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;  // make sure you add a reference to this library
using WebApplication1.Repository;

namespace WebApplication1.Model
{
    public class Event : IStorable
    {    

        public Event()
        {
            id = Guid.NewGuid().ToString("N");// assign a new Globally Unique Identifier
            EventName = string.Empty;
            EventDate = DateTime.Now;
            EventType = "";
            EventDescription = "";
            EventAttendance = 0;
            EventLocation = "";
            Materials = new List<MarketingMaterial>();
            Attendees = new List<Attendee>();
            
        }


        public string id { get; set; }


        [Required(ErrorMessage = "Name must be filled in.")]
        [Display(Description = "Event Name")]
        [StringLength(2000, MinimumLength = 5,
        ErrorMessage = "Name must be greater than {2} characters and less than {1} characters.")]
        public string EventName { get; set; }


        [Range(typeof (DateTime), "1/1/2016", "12/31/2020",
            ErrorMessage = "Event Date must be between {1} and {2}")]
        public DateTime EventDate
        {
            get { return _EventDate; }
            set
            {
                _EventDate = value;
                EventDateString = value.ToShortDateString();
            }
        }

        private DateTime _EventDate;
        public String EventDateString { get; set; }


        [Required(ErrorMessage = "Type must be filled in.")]
        [Display(Description = "Event")]
        [StringLength(2000, MinimumLength = 5,
        ErrorMessage = "Event Type must be longer than {2} characters and less than {1} characters.")]
        public string EventType { get; set; }

        [Required(ErrorMessage = "Name must be filled in.")]
        [Display(Description = "Event Description")]
        [StringLength(2000, MinimumLength = 5,
        ErrorMessage = "Description must be greater than {2} characters and less than {1} characters.")]

        public string EventDescription { get; set; }

        [Range(1, 9999, ErrorMessage = "{0} must be between {1} and {2}")]
        public int EventAttendance { get; set; }

        [Required(ErrorMessage = "Location must be filled in.")]
        [Display(Description = "Event")]
        [StringLength(2000, MinimumLength = 5,
        ErrorMessage = "Location must be greater than {2} characters and less than {1} characters.")]
        public string EventLocation { get; set; }

        


        public  List<MarketingMaterial> Materials { get; set; }
        public List<Attendee> Attendees { get; set; }

        public override string ToString()
        {
            return string.Format("Event name is {1}  introduced {2}", EventName,
                EventDate.ToShortDateString());
        }



    }
}