using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShowCase.Data.Models.ApiModels.Account
{
    public class ChangePasswordApiModel
    {
        [Required]
        [Display(Name = "Current Password")]
        public string currentPassword { get; set; }

        [Required]
        [Display(Name = "New Password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 7)]
        [DataType(DataType.Password)]
        public string newPassword { get; set; }

        [Required]
        [Display(Name = "Confirm New Password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 7)]
        [DataType(DataType.Password)]
        [Compare("newPassword", ErrorMessage = "The new password and its confirm does NOT match.")]
        public string confirmNewPassword { get; set; }
    }
}
