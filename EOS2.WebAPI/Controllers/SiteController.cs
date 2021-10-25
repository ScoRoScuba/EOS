namespace EOS2.WebAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Web.Http;
    using System.Web.Http.Description;
    using System.Web.Http.OData;

    using EOS2.WebAPI.Models;

    using Newtonsoft.Json;

    [RoutePrefix("api/v1/Site")]
    public class SiteController : ApiController
    {
        /// <summary>
        /// Gets you a list of Sites.
        /// </summary>
        [Route("")]
        [EnableQuery]
        public static IEnumerable<Site> Get()
        {
            var sites = GetSites();

            return sites;
        }

        /// <summary>
        /// Gets you a list of Sites for a Customer.
        /// </summary>
        /// <param name="customerId">Unique Identifier for Customer</param>
        [Route("~/api/v1/Customer/{customerId}/Site")]
        [EnableQuery]
        public static IEnumerable<Site> GetForCustomer(int customerId)
        {
            var sites = GetSites().Where(s => s.CustomerId == customerId);

            return sites;
        }

        /// <summary>
        /// Gets an indiviual Site by Id
        /// </summary>
        /// <param name="id">Unique Identifier for Site</param>
        [Route("{id}")]
        [ResponseType(typeof(Site))]
        public IHttpActionResult Get(int id)
        {
            var site = GetSite(id);

            if (site == null) return this.NotFound();

            return this.Ok(site);
        }

        /// <summary>
        /// Create a new Site
        /// </summary>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(Site))]
        public IHttpActionResult Post([FromBody]Site site)
        {
            if (site == null) throw new ArgumentNullException("site");

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            // TODO: The save here (just setting a dummy Id of the created instrument for now)
            site.Id = 999;

            return this.CreatedAtRoute("DefaultApi", new { controller = "site", Id = site.Id }, site);
        }

        /// <summary>
        /// Replace an existing Site.
        /// </summary>
        /// <param name="id">Unique Identifier for Site</param>
        /// <param name="site">The Site to store</param>
        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]Site site)
        {
            if (site == null) throw new ArgumentNullException("site");

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            if (id != site.Id)
            {
                return this.BadRequest("Site Id mismatch");
            }

            var databaseSite = GetSite(id);
            if (databaseSite == null)
            {
                return this.NotFound();
            }

            // TODO: Make the save
            ////Save(equipment);

            return this.StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Update an existing Site.
        /// </summary>
        /// <param name="id">Unique Identifier for Site</param>
        /// <param name="site">The items to update</param>
        [HttpPatch]
        public IHttpActionResult Patch(int id, [FromBody]Delta<Site> site)
        {
            if (site == null) throw new ArgumentNullException("site");

            // Get the instrument to update 
            var databaseSite = GetSite(id);
            if (databaseSite == null)
            {
                return this.NotFound();
            }

            // TODO: Go off and do the patch (in the service)
            site.Patch(databaseSite);
            //// TODO: Save the changed entity

            return this.StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Delete an existing site by Id.
        /// </summary>
        /// <param name="id">Unique Identifier for Site</param>
        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var site = GetSite(id);
            if (site == null)
            {
                return this.NotFound();
            }

            // TODO: Perform delete (soft)
            ////DeleteInstrument(id);

            return this.StatusCode(HttpStatusCode.NoContent);
        }

        private static Site GetSite(int id)
        {
            Site site;
            using (var sr = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Content/ApiDummyData/sites.json")))
            {
                site = JsonConvert.DeserializeObject<List<Site>>(sr.ReadToEnd()).FirstOrDefault(i => i.Id == id);
            }

            return site;
        }

        private static IEnumerable<Site> GetSites()
        {
            IEnumerable<Site> sites;
            using (var sr = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Content/ApiDummyData/sites.json")))
            {
                sites = JsonConvert.DeserializeObject<List<Site>>(sr.ReadToEnd());
            }

            return sites;
        }
    }
}