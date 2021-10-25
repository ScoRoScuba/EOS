namespace EOS2.Web.Areas.Organizations.ViewModels.Users
{
    using System.Collections.Generic;

    using EOS2.Identity.Model;
    using EOS2.Model;
    using EOS2.Web.ViewModels;

    public class UsersIndexViewModel : BaseViewModel
    {
        public IEnumerable<User> Users { get; set; }
    }
}