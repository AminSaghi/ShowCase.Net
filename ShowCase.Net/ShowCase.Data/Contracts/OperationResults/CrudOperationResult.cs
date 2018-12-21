using System;
using System.Collections.Generic;
using System.Text;

namespace ShowCase.Data.Contracts.OperationResults
{
    public class CrudOperationResult<T> : OperationResultBase
    {
        public T ReturningValue { get; set; }
    }
}
