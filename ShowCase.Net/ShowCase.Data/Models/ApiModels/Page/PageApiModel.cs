using ShowCase.Data.Models.ApiModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShowCase.Data.Models.ApiModels.Page
{
    public class PageApiModel : ContentApiModel
    {
        public string content { get; set; }

        public string createDateTime { get; set; }
        public string updateDateTime { get; set; }

        public PageApiModel parent { get; set; }
        public IEnumerable<PageApiModel> children { get; set; }
    }
}
