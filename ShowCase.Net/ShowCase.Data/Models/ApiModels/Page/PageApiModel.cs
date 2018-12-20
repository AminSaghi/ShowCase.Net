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
        public string updateDateTime { get;set;}

        public IEnumerable<ListPagesApiModel> children { get;set;}
    }
}
