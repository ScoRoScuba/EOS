namespace EOS2.Web.Areas.Organizations.Builders.Site
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;

    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Web.Areas.Organizations.ViewModels.Site;
    using EOS2.Web.Builders;

    using Common = EOS2.Web.Areas.Organizations.ViewModels.Common;

    public class SiteEditViewModelBuilder : IEditViewPartialModelBuilder<CustomerSiteEditViewModel>
    {
        private readonly ISiteService siteService;
        private readonly IPlantAreaService plantAreaService;

        public SiteEditViewModelBuilder(ISiteService siteService, IPlantAreaService plantAreaService)
        {
            if (siteService == null) throw new ArgumentNullException("siteService");
            if (plantAreaService == null) throw new ArgumentNullException("plantAreaService");

            this.siteService = siteService;
            this.plantAreaService = plantAreaService;
        }

        public CustomerSiteEditViewModel Build(int? id)
        {
            var viewModel = new CustomerSiteEditViewModel();

            if (id.HasValue) viewModel = Mapper.Map<CustomerSiteEditViewModel>(siteService.GetSite(id.Value));

            return viewModel;
        }

        public CustomerSiteEditViewModel Rebuild(CustomerSiteEditViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException("viewModel");

            if (viewModel.PlantAreas == null) viewModel.PlantAreas = Mapper.Map<IEnumerable<Common.PlantAreaViewModel>>(plantAreaService.GetPlantAreasForSite(viewModel.Id));

            return viewModel;
        }
    }
}