namespace EOS2.Web.Attributes
{
    using System;

    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Web.Areas.Organizations.ViewModels.Schedules;

    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public sealed class UniqueScheduleAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var model = (ScheduleEditViewModel)value;

                var schedulesService = DependencyResolver.Current.GetService<IScheduleService>();

                var itemFoundResult = schedulesService.ScheduleExists(model.Name, model.EquipmentId, model.Id);
                if (itemFoundResult)
                {
                    return new ValidationResult("[[[A Schedule with that Name already exists for this Equipment.]]]", new[] { "Name" });
                }

                itemFoundResult = schedulesService.ScheduleExists(model.Type.Id, model.FurnaceClass.Id, model.EquipmentId, model.Id);
                if (itemFoundResult)
                {
                    return new ValidationResult("[[[A Schedule of that Type & Furnace Class already exists for this Equipment.]]]", new[] { "Type" });
                }
            }

            return ValidationResult.Success;
        }
    }
}