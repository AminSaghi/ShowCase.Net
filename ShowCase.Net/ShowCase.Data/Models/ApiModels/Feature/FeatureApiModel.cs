using ShowCase.Data.Models.ApiModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShowCase.Data.Models.ApiModels.Feature
{
    public class FeatureApiModel : ContentApiModel
    {
        public string content { get; set; }

        public string createDateTime { get; set; }
        public string updateDateTime { get;set;}

        public IEnumerable<ListFeaturesApiModel> children { get;set;}
    }
}
