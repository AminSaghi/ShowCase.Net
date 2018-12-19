using ShowCase.Data.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShowCase.Data.Models.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public bool Published { get; set; }

        public virtual ICollection<Feature> Features { get; set; }
    }
}
