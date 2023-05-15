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
