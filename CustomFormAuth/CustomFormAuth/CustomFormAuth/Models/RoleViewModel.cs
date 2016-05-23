using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomFormAuth.Models
{
    public class RoleViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Role name is required", AllowEmptyStrings = false)]
        [Display(Name ="Role name")]
        public string RoleName { get; set; }
    }
}