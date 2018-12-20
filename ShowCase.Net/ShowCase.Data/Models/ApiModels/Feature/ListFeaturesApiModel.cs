using ShowCase.Data.Models.ApiModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShowCase.Data.Models.ApiModels.Feature
{
    public class ListFeaturesApiModel : ContentApiModel
    {
        public string updateDateTime { get;set;}

        public IEnumerable<ListFeaturesApiModel> children { get;set;}
    }
}
