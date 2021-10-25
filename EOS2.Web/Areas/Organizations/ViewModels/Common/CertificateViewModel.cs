namespace EOS2.Web.Areas.Organizations.ViewModels.Common
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using EOS2.Web.ViewModels;

    public class CertificateViewModel : BaseViewModel
    {
        public int Id { get; set; }

        [Display(Name = "[[[Certificate Number]]]")]
        public string CertificateNumber { get; set; }

        [Display(Name = "[[[End Date]]]")]
        [UIHint("ShortDate")]
        public DateTime EndDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "There is no real confusion between CertificateViewModel Type & object.GetType()")]
        [Display(Name = "[[[Type]]]")]
        public CertificateTypeViewModel Type { get; set; }
    }
}