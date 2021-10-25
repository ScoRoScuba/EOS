namespace EOS2.Web.Areas.Organizations
{
    using System;
    using System.Web.Mvc;
    
    public class OrganizationsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Organizations";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            if (context == null) throw new ArgumentNullException("context");        
    
            context.MapRoute(
                "OrganizationUsers",
                "Organizations/{organizationtype}/{organizationId}/Users/{action}/{id}",
                new { controller = "Users", action = "Index", @organizationtype = UrlParameter.Optional, @organizationId = UrlParameter.Optional, id = UrlParameter.Optional });

            context.MapRoute(
                "AddOrganizationUsers",
                "Organizations/{organizationtype}/{organizationId}/Users/Add",
                new { controller = "Users", action = "Add", @organizationtype = UrlParameter.Optional, @organizationId = UrlParameter.Optional });

            // Site
            context.MapRoute( 
                "CustomerOrganization_NewSite",
                "Organizations/Customer/{CustomerId}/Site/New",  // controller = Site           
                new { controller = "Site", action = "New" });

            context.MapRoute( 
                "CustomerOrganization_SaveSite",
                "Organizations/Customer/{CustomerId}/Site/Save",  // controller = Site           
                new { controller = "Site", action = "Save" });
 
            context.MapRoute( 
                "CustomerOrganization_Sites",
                "Organizations/Customer/{CustomerId}/Site/Index",  // controller = Site
                new { controller = "Site", action = "Index" });

            context.MapRoute(
                "CustomerOrganization_Site",
                "Organizations/Customer/{CustomerId}/Site/{SiteId}/{action}",  // controller = Site
                new { controller = "Site", action = "View" });

            // PlantArea
            context.MapRoute( 
                "CustomerOrganization_NewSitePlantArea",
                "Organizations/Customer/{CustomerId}/Site/{SiteId}/PlantArea/New",
                new { controller = "PlantArea", action = "New" });

            context.MapRoute( 
                "CustomerOrganization_SaveSitePlantArea",
                "Organizations/Customer/{CustomerId}/Site/{SiteId}/PlantArea/Save",
                new { controller = "PlantArea", action = "Save" });

            context.MapRoute( 
                "CustomerOrganization_SitePlantArea",
                "Organizations/Customer/{CustomerId}/Site/{SiteId}/PlantArea/{PlantAreaId}/{action}",
                new { controller = "PlantArea", action = "View" });

            // Equipment
            context.MapRoute( 
                "CustomerOrganization_NewSitePlantAreaEquipment",
                "Organizations/Customer/{CustomerId}/Site/{SiteId}/PlantArea/{PlantAreaId}/Equipment/New",
                new { controller = "Equipment", action = "New" });

            context.MapRoute( 
                "CustomerOrganization_SaveSitePlantAreaEquipment",
                "Organizations/Customer/{CustomerId}/Site/{SiteId}/PlantArea/{PlantAreaId}/Equipment/Save",
                new { controller = "Equipment", action = "Save" });

            context.MapRoute( 
                "CustomerOrganization_SitePlantAreaEquipmentAvailableInstruments",
                "Organizations/Customer/{CustomerId}/Site/{SiteId}/PlantArea/{PlantAreaId}/Equipment/{EquipmentId}/AvailableInstruments",
                new { controller = "Instrument", action = "AvailableInstruments" });

            context.MapRoute( 
                "CustomerOrganization_SitePlantAreaEquipment",
                "Organizations/Customer/{CustomerId}/Site/{SiteId}/PlantArea/{PlantAreaId}/Equipment/{EquipmentId}/{action}",
                new { controller = "Equipment", action = "View" });

            // Equipment Schedule
            context.MapRoute( 
                "CustomerOrganization_NewSitePlantAreaEquipmentSchedule",
                "Organizations/Customer/{CustomerId}/Site/{SiteId}/PlantArea/{PlantAreaId}/Equipment/{EquipmentId}/Schedule/New",  // controller =  Schedule            
                new { controller = "Schedule", action = "New" });

            context.MapRoute( 
                "CustomerOrganization_SaveSitePlantAreaEquipmentSchedule",
                "Organizations/Customer/{CustomerId}/Site/{SiteId}/PlantArea/{PlantAreaId}/Equipment/{EquipmentId}/Schedule/Save",  // controller =  Schedule            
                new { controller = "Schedule", action = "Save" });

            context.MapRoute( 
                "CustomerOrganization_SitePlantAreaEquipmentSchedule",
                "Organizations/Customer/{CustomerId}/Site/{SiteId}/PlantArea/{PlantAreaId}/Equipment/{EquipmentId}/Schedule/{scheduleId}/{action}",  // controller =  Schedule            
                new { controller = "Schedule", action = "View" });

            context.MapRoute( 
                "CustomerOrganization_SitePlantAreaEquipmentAvailableChannels",
                "Organizations/Customer/{CustomerId}/Site/{SiteId}/PlantArea/{PlantAreaId}/Equipment/{EquipmentId}/Instrument/{InstrumentId}/AvailableChannels/",
                new { controller = "EquipmentChannels", action = "AvailableChannels" });

            context.MapRoute( 
                "CustomerOrganization_SitePlantAreaEquipmentAllocatedChannels",
                "Organizations/Customer/{CustomerId}/Site/{SiteId}/PlantArea/{PlantAreaId}/Equipment/{EquipmentId}/Instrument/{InstrumentId}/AllocatedChannels/",
                new { controller = "EquipmentChannels", action = "View" });

            context.MapRoute( 
                "CustomerOrganization_SitePlantAreaEquipmentAllocateChannels",
                "Organizations/Customer/{CustomerId}/Site/{SiteId}/PlantArea/{PlantAreaId}/Equipment/{EquipmentId}/Instrument/{InstrumentId}/AllocateChannel/",
                new { controller = "EquipmentChannels", action = "AllocateChannel" });

            context.MapRoute( 
                "CustomerOrganization_SitePlantAreaEquipmentSaveChannels",
                "Organizations/Customer/{CustomerId}/Site/{SiteId}/PlantArea/{PlantAreaId}/Equipment/{EquipmentId}/Instrument/{InstrumentId}/SaveChannel/",
                new { controller = "EquipmentChannels", action = "SaveChannel" });

            // Channels
            context.MapRoute( 
                "CustomerOrganization_SitePlantAreaInstrumentsChannels",
                "Organizations/Customer/{CustomerId}/Site/{SiteId}/PlantArea/{PlantAreaId}/Instrument/{InstrumentId}/Channels/{action}",
                new { controller = "InstrumentChannels", action = "View" });

            context.MapRoute( 
                "CustomerOrganization_SitePlantAreaInstrumentsCreateChannels",
                "Organizations/Customer/{CustomerId}/Site/{SiteId}/PlantArea/{PlantAreaId}/Instrument/{InstrumentId}/Channels/CreateChannels",
                new { controller = "InstrumentChannels", action = "CreateChannels" });

            context.MapRoute( 
                "CustomerOrganization_SitePlantAreaInstrumentsSaveChannel",
                "Organizations/Customer/{CustomerId}/Site/{SiteId}/PlantArea/{PlantAreaId}/Instrument/{InstrumentId}/Channels/SaveChannel",
                new { controller = "InstrumentChannels", action = "SaveChannel" });

            // Instruments
            context.MapRoute( 
                "CustomerOrganization_NewSitePlantAreaInstruments",
                "Organizations/Customer/{CustomerId}/Site/{SiteId}/PlantArea/{PlantAreaId}/Instrument/New",
                new { controller = "Instrument", action = "New" });

            context.MapRoute( 
                "CustomerOrganization_SaveSitePlantAreaInstruments",
                "Organizations/Customer/{CustomerId}/Site/{SiteId}/PlantArea/{PlantAreaId}/Instrument/Save",
                new { controller = "Instrument", action = "Save" });

            context.MapRoute( 
                "CustomerOrganization_SitePlantAreaInstrument",
                "Organizations/Customer/{CustomerId}/Site/{SiteId}/PlantArea/{PlantAreaId}/Instrument/{InstrumentId}/{action}",
                new { controller = "Instrument", action = "View" });

            // Certificates
            context.MapRoute(
                "CustomerOrganization_SitePlantAreaInstrumentCertificateUpload",
                "Organizations/Customer/{CustomerId}/Site/{SiteId}/PlantArea/{PlantAreaId}/Instrument/{InstrumentId}/Certificate/Upload",
                new { controller = "Certificate", action = "Upload" });

            context.MapRoute(
                "CustomerOrganization_SitePlantAreaInstrumentCertificateDownload",
                "Organizations/Customer/{CustomerId}/Site/{SiteId}/PlantArea/{PlantAreaId}/Instrument/{InstrumentId}/Certificate/Download",
                new { controller = "Certificate", action = "Download" });

            context.MapRoute(
                "CustomerOrganization_SitePlantAreaInstrumentCertificate",
                "Organizations/Customer/{CustomerId}/Site/{SiteId}/PlantArea/{PlantAreaId}/Instrument/{InstrumentId}/Certificate/{CertificateId}/{action}",
                new { controller = "Certificate", action = "Edit" });

            // Default Route
            context.MapRoute(
                "Organizations_default",
                "Organizations/{controller}/{action}/{id}",
                new { action = "Index", controller = "Home", id = UrlParameter.Optional });
        }
    }
}