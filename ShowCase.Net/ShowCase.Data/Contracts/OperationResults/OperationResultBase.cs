using System;
using System.Collections.Generic;
using System.Text;

namespace ShowCase.Data.Contracts.OperationResults
{
    public abstract class OperationResultBase
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
