using ShowCase.Data.Models.ApiModels.Feature;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShowCase.Data.Models.ApiModels.Project
{
    public class ProjectApiModel
    {
        public int id { get; set; }

        public int orderIndex { get; set; }
        public string title { get; set; }
        public string slug { get; set; }
        public string description { get; set; }
        public string imageUrl { get; set; }

        public bool published { get; set; }

        public IEnumerable<ListFeaturesApiModel> features { get; set; }
    }
}
