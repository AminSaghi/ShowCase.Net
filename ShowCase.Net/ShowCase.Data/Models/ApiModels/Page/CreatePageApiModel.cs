using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShowCase.Data.Models.ApiModels.Page
{
    public class CreatePageApiModel
    {
        public int parentId { get; set; }

        [Required]
        public string title { get; set; }
        [Required]
        public string slug { get; set; }
        public string content { get; set; }
    }
}
