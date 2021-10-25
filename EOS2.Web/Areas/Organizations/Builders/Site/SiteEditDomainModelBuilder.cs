namespace EOS2.Web.Areas.Organizations.Builders.Site
{
    using System;

    using AutoMapper;

    using EOS2.Model;
    using EOS2.Web.Areas.Organizations.ViewModels.Site;
    using EOS2.Web.Builders;

    public class SiteEditDomainModelBuilder : IDomainModelBuilder<Site, CustomerSiteEditViewModel>
    {
        public Site Build(CustomerSiteEditViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException("viewModel");

            return Mapper.Map<Site>(viewModel);
        }
    }
}