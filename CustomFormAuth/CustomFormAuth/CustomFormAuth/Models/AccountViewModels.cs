using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomFormAuth.Models
{
    public class AccountRegisterModel
    {
        [Required(ErrorMessage ="Please enter your first name")]
        [Display(Name ="First name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter your last name")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter your email")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please create your username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please create your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match!")]
        public string ConfirmPassword { get; set; }

    }

}