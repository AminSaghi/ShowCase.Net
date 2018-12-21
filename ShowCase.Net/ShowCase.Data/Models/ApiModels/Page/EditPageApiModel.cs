using ShowCase.Data.Models.ApiModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShowCase.Data.Models.ApiModels.Page
{
    public class EditPageApiModel : ContentApiModel
    {
        public int parentId { get; set; }

        public string content { get; set; }
    }
}
