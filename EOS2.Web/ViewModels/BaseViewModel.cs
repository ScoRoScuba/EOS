namespace EOS2.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;

    using EOS2.Model.Enums;
    using EOS2.Web.Code;

    public class BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "This is a base class, its members should not be static.")]
        public bool ShowPersonas
        {
            get
            {
                var showPersona = ConfigurationManager.AppSettings["showPersonas"];
                if (!string.IsNullOrWhiteSpace(showPersona))
                {
                    bool value;

                    if (bool.TryParse(showPersona, out value))
                    {
                        return value;
                    }                
                }

                return false;
            }
        }

        public OrganizationType? OrganizationType { get; set; }

        public int? OrganizationId { get; set; }

        public string OrganizationName { get; set; }

        public string SelectedLanguageId { get; set; }

        public Uri LanguageReturnUrl { get; set; }

        public IEnumerable<Language> AvailableLanguages { get; set; }

        public int CustomerId { get; set; }

        public int SiteId { get; set; }
        
        public int PlantAreaId { get; set; }

        public int InstrumentId { get; set; }

        public int EquipmentId { get; set; }
    }
}