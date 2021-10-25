namespace EOS2.Web.Areas.Organizations.ViewModels.Shared
{
    using System.Collections.Generic;

    using EOS2.Model;
    using EOS2.Web.ViewModels;

    public class OrganizationsViewModel : BaseViewModel
    {
       public IEnumerable<Organization> Organizations { get; set; }
    }
}