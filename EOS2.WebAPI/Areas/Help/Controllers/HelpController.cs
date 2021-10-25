namespace EOS2.WebAPI.Areas.Help.Controllers
{
    using System.Web.Http;
    using System.Web.Mvc;

    using EOS2.Web.Areas.Help.ModelDescriptions;
    using EOS2.WebAPI.Areas.Help.Models;

    /// <summary>
    /// The controller that will handle requests for the help page.
    /// </summary>
    [System.Web.Mvc.AllowAnonymous]
    public class HelpController : Controller
    {
        private const string ErrorViewName = "Error";

        protected static HttpConfiguration Configuration
        {
            get { return GlobalConfiguration.Configuration; }
        }

        public ActionResult Index()
        {
            this.ViewBag.DocumentationProvider = Configuration.Services.GetDocumentationProvider();
            return this.View(Configuration.Services.GetApiExplorer().ApiDescriptions);
        }

        public ActionResult Api(string apiId)
        {
            if (!string.IsNullOrEmpty(apiId))
            {
                HelpPageApiModel apiModel = Configuration.GetHelpPageApiModel(apiId);
                if (apiModel != null)
                {
                    return this.View(apiModel);
                }
            }

            return this.View(ErrorViewName);
        }

        public ActionResult ResourceModel(string modelName)
        {
            if (!string.IsNullOrEmpty(modelName))
            {
                ModelDescriptionGenerator modelDescriptionGenerator = Configuration.GetModelDescriptionGenerator();
                ModelDescription modelDescription;
                if (modelDescriptionGenerator.GeneratedModels.TryGetValue(modelName, out modelDescription))
                {
                    return this.View(modelDescription);
                }
            }

            return this.View(ErrorViewName);
        }
    }
}