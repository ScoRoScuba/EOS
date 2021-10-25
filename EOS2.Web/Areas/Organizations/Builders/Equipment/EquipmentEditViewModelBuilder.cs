namespace EOS2.Web.Areas.Organizations.Builders.Equipment
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using EOS2.Infrastructure.Interfaces.Services;

    using EOS2.Web.Areas.Organizations.ViewModels.Equipments;
    using EOS2.Web.Areas.Organizations.ViewModels.Schedule;
    using EOS2.Web.Builders;

    using ViewModels.Common;

    public class EquipmentEditViewModelBuilder : IEditViewPartialModelBuilder<EquipmentEditViewModel>
    {
        private readonly IEquipmentService equipmentService;

        // TODO: Remove this once the field is used.
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Temporary suppression")]
        private readonly IInstrumentService instrumentService;

        private readonly IPlantAreaService plantAreaService;
        private readonly IScheduleService scheduleService;
        private readonly IReferenceDataService referenceDataService;

        public EquipmentEditViewModelBuilder(IEquipmentService equipmentService, IInstrumentService instrumentService, IPlantAreaService plantAreaService, IScheduleService scheduleService, IReferenceDataService referenceDataService)
        {
            if (equipmentService == null) throw new ArgumentNullException("equipmentService");
            if (instrumentService == null) throw new ArgumentNullException("instrumentService");
            if (scheduleService == null) throw new ArgumentNullException("scheduleService");
            if (plantAreaService == null) throw new ArgumentNullException("plantAreaService");            
            if (referenceDataService == null) throw new ArgumentNullException("referenceDataService");

            this.equipmentService = equipmentService;
            this.instrumentService = instrumentService;
            this.plantAreaService = plantAreaService;
            this.scheduleService = scheduleService;
            this.referenceDataService = referenceDataService;
        }

        public EquipmentEditViewModel Build(int? id)
        {
            var viewModel = new EquipmentEditViewModel();

            if (id.HasValue)
            {
                var equipment = equipmentService.GetEquipment(id.Value);
                viewModel = Mapper.Map<EquipmentEditViewModel>(equipment);
            }

            // Add in Reference Data
            viewModel.EquipmentTypes = this.GetEquipmentTypes();
            viewModel.AttachedInstruments = this.GetAttachedInstruments(viewModel.Id);

            if (viewModel.Id != 0)
            {
                viewModel.AllInstruments = this.GetUnattachedInstruments(viewModel);
                viewModel.Schedules = new EquipmentScheduleViewModel
                                          {
                                              Schedules = Mapper.Map<IEnumerable<ScheduleViewModel>>(scheduleService.GetSchedulesForEquipment(viewModel.Id))
                                          };
            }
            else
            {
                viewModel.AllInstruments = new List<InstrumentViewModel>();
                viewModel.Schedules = new EquipmentScheduleViewModel { Schedules = new List<ScheduleViewModel>() };
            }

            return viewModel;
        }

        public EquipmentEditViewModel Rebuild(EquipmentEditViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException("viewModel");

            if (viewModel.Id != 0)
            {
                if (viewModel.AttachedInstruments == null)
                {
                    viewModel.AttachedInstruments = GetAttachedInstruments(viewModel.Id);
                }

                if (viewModel.Schedules == null)
                {
                    viewModel.Schedules = new EquipmentScheduleViewModel
                                          {
                                              Schedules =
                                                  Mapper.Map<IEnumerable<ScheduleViewModel>>(
                                                      scheduleService
                                                  .GetSchedulesForEquipment(viewModel.Id))
                                          };
                }

                if (viewModel.AllInstruments == null)
                {
                    viewModel.AllInstruments = this.GetUnattachedInstruments(viewModel);
                }
            }

            if (viewModel.EquipmentTypes == null) viewModel.EquipmentTypes = this.GetEquipmentTypes();

            return viewModel;
        }

        private IEnumerable<EquipmentTypeViewModel> GetEquipmentTypes()
        {
            var equipmentTypes = Mapper.Map<IEnumerable<EquipmentTypeViewModel>>(referenceDataService.GetEquipmentTypes());
            return equipmentTypes.OrderBy(et => et.Name);
        }

        private IEnumerable<InstrumentViewModel> GetAttachedInstruments(int equipmentId)
        {
            var instrumentsAttachedToEquipment = equipmentService.GetInstrumentsAttachedTo(equipmentId);
                
            if (instrumentsAttachedToEquipment.Any())
            {
                return Mapper.Map<IEnumerable<InstrumentViewModel>>(instrumentsAttachedToEquipment);                
            }

            return new List<InstrumentViewModel>();
        }

        private IEnumerable<InstrumentViewModel> GetUnattachedInstruments(EquipmentEditViewModel viewModel)
        {
            var instrumentsByPlantArea = plantAreaService.GetInstrumentsFor(viewModel.PlantAreaId);

            var availableInstruments = viewModel.AttachedInstruments != null && viewModel.AttachedInstruments.Any()
                                            ? instrumentsByPlantArea.Where(
                                            i => viewModel.AttachedInstruments.Any(ia => ia.Id != i.Id)) :
                                            instrumentsByPlantArea;

            var instruments = Mapper.Map<IEnumerable<InstrumentViewModel>>(availableInstruments);

            return instruments.OrderBy(i => i.Name);
        }
    }
}