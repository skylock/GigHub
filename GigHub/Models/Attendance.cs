using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GigHub.Models
{
    public class Attendance
    {
        [Key]
        public int GigId { get; set; }
        public Gig Gig { get; set; }

        [Key, ForeignKey("ApplicationUser")]
        public string AttendeeId { get; set; }
        public ApplicationUser Attendee { get; set; }
    }
}
