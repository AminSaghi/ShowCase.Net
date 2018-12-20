using System;
using System.Collections.Generic;
using System.Text;

namespace ShowCase.Data.Models.Base
{
    public class Content
    {
        public int Id { get; set; }        

        public int OrderIndex { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }

        public bool Published { get; set; }
    }
}
