namespace EOS2.Repository 
{
    using System.Data.Entity.Core.Metadata.Edm;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.ModelConfiguration.Conventions;

    // Provides a convention for fixing the independent association (IA) foreign key column names.  
    public class ForeignKeyNamingConvention : IStoreModelConvention<AssociationType> 
    { 
        public void Apply(AssociationType item, DbModel model) 
        { 
            // Identify ForeignKey properties (including IAs)  
            if (item != null && item.IsForeignKey) 
            { 
                // rename FK columns  
                var constraint = item.Constraint; 
                if (DoPropertiesHaveDefaultNames(constraint.FromProperties, constraint.ToProperties)) 
                { 
                    NormalizeForeignKeyProperties(constraint.FromProperties); 
                }
 
                if (DoPropertiesHaveDefaultNames(constraint.ToProperties, constraint.FromProperties)) 
                { 
                    NormalizeForeignKeyProperties(constraint.ToProperties); 
                } 
            } 
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.EndsWith(System.String)", Justification = "This only applies to externally visable strings, of which this is not")]
        private static bool DoPropertiesHaveDefaultNames(ReadOnlyMetadataCollection<EdmProperty> properties,  ReadOnlyMetadataCollection<EdmProperty> otherEndProperties) 
        { 
            if (properties.Count != otherEndProperties.Count) 
            { 
                return false; 
            } 
 
            for (int i = 0; i < properties.Count; ++i) 
            { 
                if (!properties[i].Name.EndsWith("_" + otherEndProperties[i].Name)) 
                { 
                    return false; 
                } 
            }
 
            return true; 
        } 
 
        private static void NormalizeForeignKeyProperties(ReadOnlyMetadataCollection<EdmProperty> properties) 
        { 
            for (int i = 0; i < properties.Count; ++i) 
            { 
                int underscoreIndex = properties[i].Name.IndexOf('_'); 
                if (underscoreIndex > 0) 
                { 
                    properties[i].Name = properties[i].Name.Remove(underscoreIndex, 1); 
                }                 
            } 
        } 
    }
}