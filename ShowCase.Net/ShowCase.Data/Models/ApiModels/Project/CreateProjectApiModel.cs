using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShowCase.Data.Models.ApiModels.Project
{
    public class CreateProjectApiModel
    {
        [Required]
        public string title { get; set; }
        [Required]
        public string slug { get; set; }
        public string description { get; set; }
        public string imageUrl { get; set; }
    }
}
