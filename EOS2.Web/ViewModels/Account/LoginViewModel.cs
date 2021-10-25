namespace EOS2.Web.ViewModels.Account
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class LoginViewModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}