using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace GigHub.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() : base()
        {

        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public ICollection<Attendance> Attendances { get; set; }
    }
}
