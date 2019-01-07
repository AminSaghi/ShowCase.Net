using System;
using System.Collections.Generic;
using System.Text;

namespace ShowCase.Data.Models.ApiModels.User
{
    public class ListUsersApiModel
    {
        public string id { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string status { get; set; }
    }
}
