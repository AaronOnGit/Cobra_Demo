using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomFormAuth.Models
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "Username required.", AllowEmptyStrings = false)]
        [Display(Name = "Email/username")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Username required.", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember me?")]
        public bool Remember { get; set; }
    }
}