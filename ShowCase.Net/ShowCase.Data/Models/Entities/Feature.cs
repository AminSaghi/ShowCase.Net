using ShowCase.Data.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShowCase.Data.Models.Entities
{
    public class Feature : Content
    {
        public string Content { get; set; }

        public DateTimeOffset CreateDateTime { get; set; }
        public DateTimeOffset UpdateDateTime { get; set; }

        public virtual Project Project { get; set; }
        public virtual Feature Parent { get; set; }
        public virtual ICollection<Feature> Children { get; set; }
    }
}
