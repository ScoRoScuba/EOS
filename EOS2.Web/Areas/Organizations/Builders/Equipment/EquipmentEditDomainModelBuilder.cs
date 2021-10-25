namespace EOS2.Web.Areas.Organizations.Builders.Equipment
{
    using AutoMapper;

    using EOS2.Model;
    using EOS2.Web.Areas.Organizations.ViewModels.Equipments;
    using EOS2.Web.Builders;

    public class EquipmentEditDomainModelBuilder : IDomainModelBuilder<Equipment, EquipmentEditViewModel>
    {
        public Equipment Build(EquipmentEditViewModel viewModel)
        {
            return Mapper.Map<Equipment>(viewModel);
        }
    }
}