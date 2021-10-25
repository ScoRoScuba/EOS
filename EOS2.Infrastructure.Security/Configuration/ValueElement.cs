namespace EOS2.Infrastructure.Security.Configuration
{
    using System.Configuration;

    public class ValueElement : ConfigurationElement
    {
        protected const string ValueProperty = "value";

        [ConfigurationProperty(ValueProperty, IsRequired = false)]
        public string Value        
        {
            get { return (string)this[ValueProperty]; }
            set { this[ValueProperty] = value; }
        }
    }
}
