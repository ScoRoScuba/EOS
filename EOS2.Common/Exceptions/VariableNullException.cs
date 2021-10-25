namespace EOS2.Common
{
    using System;
    using System.Runtime.Serialization;

    // This is for use where a null value is detected, but it wasn't passed in as an argument.
    [Serializable]
    public class VariableNullException : ArgumentNullException
    {
        public VariableNullException()
            : base()
        {
        }

        public VariableNullException(string variableName)
            : base(variableName)
        {
        }

        public VariableNullException(string variableName, Exception exception)
            : base(variableName, exception)
        {
        }

        protected VariableNullException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}
