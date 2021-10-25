namespace EOS2.Web.Areas.Organizations.Controllers
{
    using System;
    using System.Globalization;
    using System.Web.Mvc;

    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model;
    using EOS2.Web.Areas.Organizations.ViewModels.Schedules;
    using EOS2.Web.Builders;
    using EOS2.Web.Code;
    using EOS2.Web.Filters;

    public class ScheduleController : BaseController
    {
       private readonly IScheduleService scheduleService;
       private readonly IEditViewPartialModelBuilder<ScheduleEditViewModel> editScheduleViewModelBuilder;
       private readonly IDomainModelBuilder<Schedule, ScheduleEditViewModel> editScheduleDomainModelBuilder;

       public ScheduleController(IScheduleService scheduleService, IEditViewPartialModelBuilder<ScheduleEditViewModel> editScheduleViewModelBuilder, IDomainModelBuilder<Schedule, ScheduleEditViewModel> editScheduleDomainModelBuilder)
        {
            if (scheduleService == null) throw new ArgumentNullException("scheduleService");
            if (editScheduleViewModelBuilder == null) throw new ArgumentNullException("editScheduleViewModelBuilder");
            if (editScheduleDomainModelBuilder == null) throw new ArgumentNullException("editScheduleDomainModelBuilder");

            this.scheduleService = scheduleService;
            this.editScheduleViewModelBuilder = editScheduleViewModelBuilder;
            this.editScheduleDomainModelBuilder = editScheduleDomainModelBuilder;
        }

       public ActionResult New(int equipmentId)
       {
           var viewModel = editScheduleViewModelBuilder.Build(null);

           viewModel.EquipmentId = equipmentId;

           return View("Edit", viewModel);
       }

       [HttpPost]
       public ActionResult Save(ScheduleEditViewModel viewModel)
       {
           if (viewModel == null) throw new ArgumentNullException("viewModel");

           if (!this.ModelState.IsValid)
           {
               // We need to repopulate the reference data before we can reshow he view
               editScheduleViewModelBuilder.Rebuild(viewModel);

               return View("Edit", viewModel);
           }

           var schedule = editScheduleDomainModelBuilder.Build(viewModel);
           scheduleService.SaveSchedule(schedule);

           TempData["ControllerActionMessageSchedule"] = string.Format(CultureInfo.CurrentUICulture, "[[[Schedule %0 saved successfully|||{0}]]]", schedule.Name);

           return RedirectToRoute(
                            "CustomerOrganization_SitePlantAreaEquipment", 
                            new
                                {
                                    viewModel.CustomerId,
                                    viewModel.SiteId,
                                    viewModel.PlantAreaId,
                                    viewModel.EquipmentId
                                });
       }

       public ActionResult View(int scheduleId)
       {
           var viewModel = editScheduleViewModelBuilder.Build(scheduleId);

           return View("Edit", viewModel);
       }
    }
}