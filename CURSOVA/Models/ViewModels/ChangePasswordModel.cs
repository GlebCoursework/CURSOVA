using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CURSOVA.Models.ViewModels
{
    public class ChangePasswordModel
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmedPassword { get; set; }
    }
}