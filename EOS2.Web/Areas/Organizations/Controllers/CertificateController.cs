namespace EOS2.Web.Areas.Organizations.Controllers
{
    using System;
    using System.Configuration;
    using System.Globalization;
    using System.Web.Mvc;
    
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model;
    using EOS2.Web.Areas.Organizations.Builders.Customer;
    using EOS2.Web.Areas.Organizations.ViewModels.Certificate;
    using EOS2.Web.Builders;
    using EOS2.Web.Code;
    using EOS2.Web.Enums;
    using EOS2.Web.Filters;

    using Microsoft.AspNet.Identity;

    using Enums = EOS2.Model.Enums;

    public class CertificateController : BaseController
    {
        private readonly ICertificateService certificateService;
        private readonly IDomainModelBuilder<CertificateHeader, CertificateEditViewModel> editCertificateDomainModelBuilder;
        private readonly IEditViewPartialModelBuilder<CertificateEditViewModel> editCertificateViewModelBuilder;
        private readonly IViewModelWithQueryBuilder<CertificateEditViewModel> createCertificateViewModelBuilder;

        public CertificateController(
            ICertificateService certificateService, 
            IDomainModelBuilder<CertificateHeader, CertificateEditViewModel> editCertificateDomainModelBuilder, 
            IEditViewPartialModelBuilder<CertificateEditViewModel> editCertificateViewModelBuilder, 
            IViewModelWithQueryBuilder<CertificateEditViewModel> createCertificateViewModelBuilder)
        {
            if (certificateService == null) throw new ArgumentNullException("certificateService");
            if (editCertificateDomainModelBuilder == null) throw new ArgumentNullException("editCertificateDomainModelBuilder");
            if (editCertificateViewModelBuilder == null) throw new ArgumentNullException("editCertificateViewModelBuilder");
            if (createCertificateViewModelBuilder == null) throw new ArgumentNullException("createCertificateViewModelBuilder");

            this.certificateService = certificateService;
            this.editCertificateDomainModelBuilder = editCertificateDomainModelBuilder;
            this.editCertificateViewModelBuilder = editCertificateViewModelBuilder;
            this.createCertificateViewModelBuilder = createCertificateViewModelBuilder;
        }

        public ActionResult Upload(Enums.CertificateType certificateType, int instrumentId)
        {
            var viewModel = createCertificateViewModelBuilder.Build(new CertificateCreateCriteria(certificateType, instrumentId));

            return View("CertificateUpload", viewModel);
        }

        public ActionResult Edit(int certificateId)
        {
            var viewModel = editCertificateViewModelBuilder.Build(certificateId);
            return View("CertificateUpload", viewModel);
        }

        [HttpPost]
        public ActionResult Upload(CertificateEditViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException("viewModel");

            if (!this.ModelState.IsValid)
            {
                viewModel = editCertificateViewModelBuilder.Rebuild(viewModel);
                return View("CertificateUpload", viewModel);
            }

            if (viewModel.DetailViewModel.File != null)
            {
                viewModel.DetailViewModel.FileUploadUserName = User.Identity.GetUserName();
            }

            var certificate = editCertificateDomainModelBuilder.Build(viewModel);
            certificateService.Save(certificate);

            if (viewModel.Id == 0)
            {
                SetActionMessage("ControllerActionMessageCertificates", string.Format(CultureInfo.CurrentCulture, "[[[Certificate %0 uploaded successfully|||{0}]]]", certificate.CertificateNumber));
            }
            else
            {
                SetActionMessage("ControllerActionMessageCertificates", string.Format(CultureInfo.CurrentCulture, "[[[Certificate %0 saved successfully|||{0}]]]", certificate.CertificateNumber));                
            }

            return RedirectToRoute(
                "CustomerOrganization_SitePlantAreaInstrument",
                new { viewModel.CustomerId, viewModel.SiteId, viewModel.PlantAreaId, viewModel.InstrumentId });
        }

        public ActionResult Download(int certificateId, ControllerAction controllerAction)
        {
            var certificate = certificateService.GetCertificate(certificateId);
            var file = certificate.CertificateBody.File;
            var fileName = certificate.CertificateBody.FileName;

            if (file == null)
            {
                var message = string.Format(CultureInfo.CurrentCulture, "[[[There was an issue downloading certificate %0 |||{0}]]]", certificate.CertificateNumber);
                switch (controllerAction)
                {
                    case ControllerAction.CertificateCertificateEdit:
                        SetActionMessage("ControllerActionMessageCertificateFailure", message);
                        return RedirectToAction("Edit", new { certificateId = certificateId });
                    case ControllerAction.InstrumentViewInstrument:
                        SetActionMessage("ControllerActionMessageCertificatesFailure", message);
                        return RedirectToAction("View", "Instrument");
                }
            }

            return Show(file, fileName);
        }

        private static ActionResult Show(byte[] fileContents, string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                filename = ConfigurationManager.AppSettings["CertificateDownloadDefaultFilename"];
            }

            var fileResult = new FileContentResult(fileContents, "application/pdf")
            {
                FileDownloadName = filename
            };

            return fileResult;
        }
    }
}