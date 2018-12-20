using System;
using System.Collections.Generic;
using System.Text;

namespace ShowCase.Data.Models.ApiModels.Project
{
    public class EditProjectApiModel
    {
        public int id { get; set; }

        public int orderIndex { get; set; }
        public string title { get; set; }
        public string slug { get; set; }
        public string description { get; set; }
        public string imageUrl { get; set; }

        public bool published { get; set; }
    }
}
