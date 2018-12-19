using ShowCase.Data.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShowCase.Data.Models.Entities
{
    public class Page : Content
    {
        public string Content { get;set;}

        public DateTimeOffset CreateDateTime { get; set; }
        public DateTimeOffset UpdateDateTime { get; set; }

        public virtual Page Parent { get; set; }
        public ICollection<Page> Children { get; set; }
    }
}
