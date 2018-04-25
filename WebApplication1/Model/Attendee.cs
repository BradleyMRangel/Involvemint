using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication1.Repository;

namespace WebApplication1.Model
{
    public class Attendee : IStorable
    {
        public Attendee()
        {
            id = Guid.NewGuid().ToString("N");

        }

        public string id { get; set; }

        [Required(ErrorMessage = "First Name field must be filled in.")]
        [Display(Description = "Name")]
        [StringLength(20, MinimumLength = 2,
        ErrorMessage = "First Name must be longer than {2} characters and less than {1} characters.")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "Last Name field must be filled in.")]
        [Display(Description = "Name")]
        [StringLength(30, MinimumLength = 2,
        ErrorMessage = "First Name must be longer than {2} characters and less than {1} characters.")]
        public string lastName { get; set; }
        public Event EventAttendee { get; set; }
        public String AttendeeResource { get; set; }

        public static Attendee Copy(Attendee attendees)
        {
            return new Attendee()
            {
                firstName = attendees.firstName,
                lastName = attendees.lastName,
                EventAttendee = attendees.EventAttendee,
                AttendeeResource = attendees.AttendeeResource
            };
        }


    }
}