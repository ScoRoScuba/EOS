namespace EOS2.Web.Areas.Help.ModelDescriptions
{
    using System.Collections.ObjectModel;

    public class ComplexTypeModelDescription : ModelDescription
    {
        public ComplexTypeModelDescription()
        {
            Properties = new Collection<ParameterDescription>();
        }

        public Collection<ParameterDescription> Properties { get; private set; }
    }
}