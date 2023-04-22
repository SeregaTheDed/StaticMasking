using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticMaskingLibrary.MaskingClasses.MaskingResults
{
    public class FailedMaskingResultWithException : FailedMaskingResult
    {
        public Exception ResultException { get; }
        public FailedMaskingResultWithException(Exception e)
        {
            ResultException = e;
        }
    }
}
