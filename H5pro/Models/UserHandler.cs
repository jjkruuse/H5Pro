using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace H5pro.Models
{
    public class UserHandler
    {
        [Required(ErrorMessage = "A user name is required")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "User name must be between 3 and 25 characters. ")]
        public string username { get; set; }

        [StringLength(50, MinimumLength = 5, ErrorMessage = "Please insert a valid email address. ")]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        [RegularExpression("((?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{7,20})",ErrorMessage = "Password must be between 7 and characters, and indclude upper and lower case, a number and a special character. " )]
        public string password { get; set; }

        [Required]
        [Compare("password", ErrorMessage = "Password mismatch. ")]
        public string passTest { get; set; }

        [Range(18, 99, ErrorMessage = "Age must be between 18 and 99.")]
        [Required]
        public int age { get; set; }

        [Required(ErrorMessage = "Please select a gender.")]
        public int gender { get; set; }
    }
}