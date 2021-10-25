namespace EOS2.WebAPI.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Description;
    using System.Web.Http.OData;

    using EOS2.WebAPI.Models;

    using Newtonsoft.Json;

    [RoutePrefix("api/v1")]
    public class UserController : ApiController
    {
        /// <summary>
        /// Gets you a list of eCAT Engineers.
        /// </summary>
        
        // [ApiExplorerSettings(IgnoreApi = true)]   TODO: This setting 'hides' this action from the help documentation, might want to comment it back in prior to real go live as its eCat specific
        [Route("eCATEngineer")]
        [EnableQuery]
        public static IEnumerable<Engineer> Get()
        {
            var engineers = GetEngineers();

            return engineers;
        }

        /// <summary>
        /// Gets you a list of eCAT Engineers for a Service Provider.
        /// </summary>
        
        // [ApiExplorerSettings(IgnoreApi = true)]   TODO: This setting 'hides' this action from the help documentation, might want to comment it back in prior to real go live as its eCat specific
        [Route("ServiceProvider/{serviceProviderId}/eCATEngineer")]
        [EnableQuery]
        public static IEnumerable<Engineer> GetForServiceProvider(int serviceProviderId)
        {
            var engineers = GetEngineers().Where(e => e.ServiceProviderId == serviceProviderId);

            return engineers;
        }

        /// <summary>
        /// Gets an indiviual eCAT Engineer by Id
        /// </summary>
        /// <param name="id">Unique Identifier for eCAT Engineer</param>
        
        // [ApiExplorerSettings(IgnoreApi = true)]   TODO: This setting 'hides' this action from the help documentation, might want to comment it back in prior to real go live as its eCat specific
        [Route("eCATEngineer/{id}")]
        [ResponseType(typeof(Engineer))]
        public IHttpActionResult Get(int id)
        {
            var engineer = GetEngineer(id);

            if (engineer == null) return this.NotFound();

            return this.Ok(engineer);
        }

        private static Engineer GetEngineer(int id)
        {
            Engineer engineer;
            using (var sr = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Content/ApiDummyData/engineers.json")))
            {
                engineer = JsonConvert.DeserializeObject<List<Engineer>>(sr.ReadToEnd()).FirstOrDefault(i => i.Id == id);
            }

            return engineer;
        }

        private static IEnumerable<Engineer> GetEngineers()
        {
            IEnumerable<Engineer> engineers;
            using (var sr = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Content/ApiDummyData/engineers.json")))
            {
                engineers = JsonConvert.DeserializeObject<List<Engineer>>(sr.ReadToEnd());
            }

            return engineers;
        }
    }
}