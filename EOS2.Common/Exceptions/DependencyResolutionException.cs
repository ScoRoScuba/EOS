namespace EOS2.Common.Exceptions
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors", Justification = "Requirement is to have the type")] 
    [Serializable]
    public class DependencyResolutionException : Exception
    {
        public DependencyResolutionException() : base()
        {
        }

        public DependencyResolutionException(Type type)
            : base(string.Format(CultureInfo.CurrentUICulture, "Unable to resolve requested type {0}", type == null ? "Unknown type" : type.FullName))
        {
            Type = type;
        }

        public DependencyResolutionException(Type type, string instanceName)
            : base(string.Format(CultureInfo.CurrentUICulture, "Unable to resolve requested type {0} for instance {1}", type == null ? "Unknown type" : type.FullName, instanceName))
        {
            Type = type;
            this.InstanceName = instanceName;
        }

        public DependencyResolutionException(Type type, string instanceName, Exception exception)
            : base(string.Format(CultureInfo.CurrentUICulture, "Unable to resolve requested type {0} for instance {1}", type == null ? "Unknown type" : type.FullName, instanceName), exception)
        {
            Type = type;
            this.InstanceName = instanceName;
        }

        protected DependencyResolutionException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
            this.Type = (Type)serializationInfo.GetValue("Type", typeof(Type));
            this.InstanceName = serializationInfo.GetString("InstanceName");
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "Type is expected value")]
        public Type Type { get; private set; }

        public string InstanceName { get; private set; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            info.AddValue("Type", this.Type);
            info.AddValue("InstanceName", this.InstanceName);

            base.GetObjectData(info, context);
        }
    }
}
