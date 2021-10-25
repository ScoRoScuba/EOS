namespace EOS2.Web.Areas.Organizations.ViewModels.Certificate
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EOS2.Web.Areas.Organizations.ViewModels.Common;
    using EOS2.Web.Attributes;
    using EOS2.Web.Enums;
    using EOS2.Web.ViewModels;

    [UniqueCertificateNumber]
    [CertificateUpload]
    public class CertificateEditViewModel : BaseViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "[[[Please enter a Certificate Number]]]")]
        [Display(Name = "[[[Certificate Number]]]")]
        public string CertificateNumber { get; set; }

        [Display(Name = "[[[Start Date]]]", Prompt = "[[[Start Date of the Certificate]]]")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "[[[Please enter a Start Date]]]")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "[[[End Date]]]", Prompt = "[[[End Date of the Certificate]]]")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "[[[Please enter an End Date]]]")]
        [CompareTo("StartDate", OperatorName = CompareToOperator.GreaterThanOrEqual, ErrorMessage = "[[[End Date must be after Start Date]]]")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "[[[Certificate]]]")]
        public CertificateDetailViewModel DetailViewModel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "There is no real confusion between CertificateViewModel Type & object.GetType()")] 
        [Required(ErrorMessage = "[[[Certificate Type must be selected]]]")]
        [Display(Name = "[[[Certificate Type]]]")]
        public CertificateTypeViewModel Type { get; set; }

        public IEnumerable<CertificateTypeViewModel> CertificateTypes { get; set; }
    }
}