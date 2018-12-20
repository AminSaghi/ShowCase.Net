using System;
using System.Collections.Generic;
using System.Text;

namespace ShowCase.Data.Models.ApiModels.Page
{
    public class CreatePageApiModel
    {
        public int parentId { get; set; }

        public string title { get; set; }
        public string slug { get; set; }
        public string content { get; set; }
    }
}
