namespace EOS2.Web.Areas.Organizations.Builders.Customer
{
    using System;
    using System.IO;

    using AutoMapper;

    using EOS2.Model;
    using EOS2.Web.Areas.Organizations.ViewModels.Certificate;
    using EOS2.Web.Builders;

    public class CertificateEditDomainModelBuilder : IDomainModelBuilder<CertificateHeader, CertificateEditViewModel>
    {
        public CertificateHeader Build(CertificateEditViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException("viewModel");

            var certificate = Mapper.Map<CertificateHeader>(viewModel);
            if (viewModel.DetailViewModel.File != null)
            { 
                certificate.CertificateBody = Mapper.Map<CertificateBody>(viewModel.DetailViewModel);
                certificate.CertificateBody.FileName = Path.GetFileName(viewModel.DetailViewModel.File.FileName);
                certificate.CertificateBody.Id = certificate.Id;
            }

            return certificate;
        }
    }
}