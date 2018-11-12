using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CURSOVA.Models.ViewModels
{
    public class ChangeEmailModel
    {
        [Required]
        //[StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [EmailAddress]
        public string NewEmail { get; set; }

        [Required]
        public string Password { get; set; }
    }
}