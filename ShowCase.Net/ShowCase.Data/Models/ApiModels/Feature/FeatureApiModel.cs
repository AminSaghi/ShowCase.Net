﻿using ShowCase.Data.Models.ApiModels.Base;
using ShowCase.Data.Models.ApiModels.Project;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShowCase.Data.Models.ApiModels.Feature
{
    public class ListFeatureApiModel : ContentApiModel
    {
        public string content { get; set; }

        public string createDateTime { get; set; }
        public string updateDateTime { get;set;}

        public ListProjectsApiModel project { get; set; }
        public IEnumerable<ListFeaturesApiModel> children { get;set;}
    }
}
