namespace EOS2.Common.Validation
{
    using System;
    using System.Collections.ObjectModel;

    public class ErrorStateCollection : Collection<ErrorState> 
    {
        public void Add(Exception exception)
        {
            Add(new ErrorState(exception));
        }

        public void Add(string errorMessage)
        {
            Add(new ErrorState(errorMessage));
        }
    }
}
