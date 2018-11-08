using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() : base()
        {
            Followers = new Collection<Following>();
            Followees = new Collection<Following>();
        }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public ICollection<Attendance> Attendances { get; set; }
        public ICollection<Following> Followers { get; set; }
        public ICollection<Following> Followees { get; set; }
    }
}
