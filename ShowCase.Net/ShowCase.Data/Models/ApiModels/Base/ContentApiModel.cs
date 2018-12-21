using System;
using System.Collections.Generic;
using System.Text;

namespace ShowCase.Data.Models.ApiModels.Base
{
    public class ContentApiModel
    {
        public int id { get; set; }

        public int orderIndex { get; set; }
        public string slug { get; set; }
        public string title { get; set; }

        public bool published { get; set; }
    }
}
