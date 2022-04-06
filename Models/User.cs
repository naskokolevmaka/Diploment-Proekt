using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Airport.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the User class
    public class User : IdentityUser
    {
        [Display(Name = "Име")]
        public string FirstName { get; set; }
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [Display(Name = "Имейл")]
        override public string Email { get; set; }

        [Display(Name = "Телфон за връзка")]
        public string Phone { get; set; }
    }
}
