using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShowCase.Data.Contracts.OperationResults
{
    public class AuthOperationResult<T> : CrudOperationResult<T>
    {        
        public IdentityResult IdentityResult { get; set; }
    }
}
