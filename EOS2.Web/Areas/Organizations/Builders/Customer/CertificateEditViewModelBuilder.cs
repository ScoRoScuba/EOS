namespace EOS2.Web.Areas.Organizations.Builders.Customer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Web.Areas.Organizations.ViewModels.Certificate;
    using EOS2.Web.Builders;

    using Common = ViewModels.Common;

    public class CertificateEditViewModelBuilder : IEditViewPartialModelBuilder<CertificateEditViewModel>, IViewModelWithQueryBuilder<CertificateEditViewModel>
    {
        private readonly ICertificateService certificateService;

        private readonly IReferenceDataService referenceDataService;

        public CertificateEditViewModelBuilder(ICertificateService certificateService, IReferenceDataService referenceDataService)
        {
            if (certificateService == null) throw new ArgumentNullException("certificateService");
            if (referenceDataService == null) throw new ArgumentNullException("referenceDataService");

            this.certificateService = certificateService;
            this.referenceDataService = referenceDataService;
        }

        public CertificateEditViewModel Build(IBuilderCriteria criteria)
        {
            if (criteria == null) throw new ArgumentNullException("criteria");

            var certificateCriteria = criteria as CertificateCreateCriteria;
            if (certificateCriteria == null) throw new InvalidCastException("[[[wrong criteria type]]]");

            return new CertificateEditViewModel
            {
                InstrumentId = certificateCriteria.InstrumentId,
                Type = new Common.CertificateTypeViewModel { Id = (int)certificateCriteria.CertificateType }
            };
        }

        public CertificateEditViewModel Build(int? id)
        {
            var viewModel = new CertificateEditViewModel();

            if (id.HasValue) viewModel = Mapper.Map<CertificateEditViewModel>(certificateService.GetCertificate(id.Value));

            // Add in Reference Data
            viewModel.CertificateTypes = GetCertificateTypes();

            return viewModel;
        }

        public CertificateEditViewModel Rebuild(CertificateEditViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException("viewModel");

            viewModel.CertificateTypes = GetCertificateTypes();

            return viewModel;
        }

        private IEnumerable<Common.CertificateTypeViewModel> GetCertificateTypes()
        {
            var certificateTypes = Mapper.Map<IEnumerable<Common.CertificateTypeViewModel>>(referenceDataService.GetInstrumentCertificateTypes());
            return certificateTypes.OrderBy(et => et.Name);
        }
    }
}