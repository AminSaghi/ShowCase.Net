using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShowCase.Data.Models.ApiModels.Project
{
    public class EditProjectApiModel
    {
        [Required]
        public int id { get; set; }

        public int orderIndex { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string slug { get; set; }
        public string description { get; set; }
        public string imageUrl { get; set; }

        public bool published { get; set; }
    }
}
