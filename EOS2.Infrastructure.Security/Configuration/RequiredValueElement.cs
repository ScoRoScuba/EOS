namespace EOS2.Infrastructure.Security.Configuration
{
    using System.Configuration;

    public class RequiredValueElement : ConfigurationElement
    {
        protected const string ValueProperty = "value";

        [ConfigurationProperty(ValueProperty, IsRequired = true)]
        public string Value        
        {
            get { return (string)this[ValueProperty]; }
            set { this[ValueProperty] = value; }
        }
    }
}
