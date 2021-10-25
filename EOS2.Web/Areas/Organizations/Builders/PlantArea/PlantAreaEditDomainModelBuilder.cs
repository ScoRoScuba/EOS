namespace EOS2.Web.Areas.Organizations.Builders.PlantArea
{
    using AutoMapper;

    using EOS2.Model;
    using EOS2.Web.Areas.Organizations.ViewModels.PlantAreas;
    using EOS2.Web.Builders;

    public class PlantAreaEditDomainModelBuilder : IDomainModelBuilder<PlantArea, PlantAreaEditViewModel>
    {
        public PlantArea Build(PlantAreaEditViewModel viewModel)
        {
            return Mapper.Map<PlantArea>(viewModel);
        }
    }
}