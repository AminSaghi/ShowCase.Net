using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShowCase.Data.Models.ApiModels.Base
{
    public class ContentApiModel
    {
        public int id { get; set; }

        public int orderIndex { get; set; }
        [Required]
        public string slug { get; set; }
        [Required]
        public string title { get; set; }

        public bool published { get; set; }
    }
}
