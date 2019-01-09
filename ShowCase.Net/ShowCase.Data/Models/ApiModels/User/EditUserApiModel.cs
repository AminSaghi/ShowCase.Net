using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShowCase.Data.Models.ApiModels.User
{
    public class EditUserApiModel
    {
        [Required]
        public string id { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string userName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string email { get; set; }
    }
}
