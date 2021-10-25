namespace EOS2.Web.Areas.Organizations.ViewModels.Certificate
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class CertificateDetailViewModel
    {
        public int Id { get; set; }

        public string FileUploadUserName { get; set; }

        [Display(Name = "[[[Certificate]]]")]
        public HttpPostedFileBase File { get; set; }
    }
}