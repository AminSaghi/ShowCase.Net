using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShowCase.Data.Models.ApiModels.User
{
    public class CreateUserApiModel
    {
        [Required]
        [Display(Name = "Username")]
        public string userName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string password { get; set; }

        [Required]
        [Compare("password")]
        [Display(Name = "Confirm Password")]
        public string confirmPassword { get; set; }
    }
}
