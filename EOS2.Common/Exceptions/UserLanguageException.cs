namespace EOS2.Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class UserLanguageException : Exception
    {
        public UserLanguageException()
            : base()
        {
        }

        public UserLanguageException(string message)
            : base(message)
        {
        }

        public UserLanguageException(string message, Exception exception)
            : base(message, exception)
        {
        }

        protected UserLanguageException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
