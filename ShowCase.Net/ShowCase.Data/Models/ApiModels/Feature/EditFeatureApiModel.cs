using ShowCase.Data.Models.ApiModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShowCase.Data.Models.ApiModels.Feature
{
    public class EditFeatureApiModel : ContentApiModel
    {
        [Required]
        public int projectId { get; set; }
        public int parentId { get; set; }

        public string content { get; set; }
    }
}
