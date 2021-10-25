namespace EOS2.Common.Validation
{
    using System;

    public class ErrorState
    {
        public ErrorState(Exception exception)
            : this(exception, null /* errorMessage */)
        {
        }

        public ErrorState(Exception exception, string errorMessage)
            : this(errorMessage)
        {
            if (exception == null)
            {
                throw new ArgumentNullException("exception");
            }

            Exception = exception;
        }

        public ErrorState(string errorMessage)
        {
            ErrorMessage = errorMessage ?? string.Empty;
        }

        public Exception Exception { get; private set; }

        public string ErrorMessage { get; private set; }
    }
}
