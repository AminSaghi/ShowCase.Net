using ShowCase.Data.Models.ApiModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShowCase.Data.Models.ApiModels.Page
{
    public class ListPagesApiModel : ContentApiModel
    {
        public string updateDateTime { get;set;}

        public IEnumerable<ListPagesApiModel> children { get;set;}
    }
}
