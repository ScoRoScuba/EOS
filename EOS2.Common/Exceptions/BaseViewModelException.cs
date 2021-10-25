namespace EOS2.Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class BaseViewModelException : Exception
    {
        public BaseViewModelException()
            : base()
        {
        }

        public BaseViewModelException(string message)
            : base(message)
        {
        }

        public BaseViewModelException(string message, Exception exception)
            : base(message, exception)
        {
        }

        protected BaseViewModelException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
