using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace GigHub.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() :base()
        {
            
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
