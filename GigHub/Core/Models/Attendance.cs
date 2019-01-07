using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Core.Models
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
