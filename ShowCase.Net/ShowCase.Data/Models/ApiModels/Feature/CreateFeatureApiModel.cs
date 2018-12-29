using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShowCase.Data.Models.ApiModels.Feature
{
    public class CreateFeatureApiModel
    {
        [Required]
        public int projectId { get; set; }
        public int parentId { get; set; }

        [Required]
        public string title { get; set; }
        [Required]
        public string slug { get; set; }
        public string content { get; set; }
    }
}
