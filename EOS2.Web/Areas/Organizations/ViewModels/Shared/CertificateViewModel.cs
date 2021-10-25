namespace EOS2.Web.Areas.Organizations.ViewModels.Shared
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

        [Display(Name = "[[[Type]]]")]
        public CertificateTypeViewModel Type { get; set; }
    }
}