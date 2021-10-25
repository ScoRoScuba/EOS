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

    [RoutePrefix("api/v1/PlantArea")]
    public class PlantAreaController : ApiController
    {
        /// <summary>
        /// Gets you a list of Plant Areas.
        /// </summary>
        [Route("")]
        [EnableQuery]
        public static IEnumerable<PlantArea> Get()
        {
            var plantAreas = GetPlantAreas();

            return plantAreas;
        }

        /// <summary>
        /// Gets you a list of Plant Areas within a Site.
        /// </summary>
        /// <param name="siteId">Unique Identifier for Site</param>
        [Route("~/api/v1/Site/{siteId}/PlantArea")]

        [EnableQuery]
        public static IEnumerable<PlantArea> GetForSite(int siteId)
        {
            var plantAreas = GetPlantAreas().Where(p => p.SiteId == siteId);

            return plantAreas;
        }

        /// <summary>
        /// Gets an indiviual Plant Area by Id
        /// </summary>
        /// <param name="id">Unique Identifier for Plant Area</param>
        [Route("{id}")]
        [ResponseType(typeof(PlantArea))]
        public IHttpActionResult Get(int id)
        {
            var plantArea = GetPlantArea(id);

            if (plantArea == null) return this.NotFound();

            return this.Ok(plantArea);
        }

        /// <summary>
        /// Create a new Plant Area
        /// </summary>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(PlantArea))]
        public IHttpActionResult Post([FromBody]PlantArea plantArea)
        {
            if (plantArea == null) throw new ArgumentNullException("plantArea");

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            // TODO: The save here (just setting a dummy Id of the created instrument for now)
            plantArea.Id = 999;

            return this.CreatedAtRoute("DefaultApi", new { controller = "plantArea", Id = plantArea.Id }, plantArea);
        }

        /// <summary>
        /// Replace an existing Plant Area.
        /// </summary>
        /// <param name="id">Unique Identifier for Equipment</param>
        /// <param name="plantArea">The Plant Area to store</param>
        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]PlantArea plantArea)
        {
            if (plantArea == null) throw new ArgumentNullException("plantArea");
            
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            if (id != plantArea.Id)
            {
                return this.BadRequest("Plant Area Id mismatch");
            }

            var databasePlantArea = GetPlantArea(id);
            if (databasePlantArea == null)
            {
                return this.NotFound();
            }

            // TODO: Make the save
            ////Save(equipment);

            return this.StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Update an existing Plant Area.
        /// </summary>
        /// <param name="id">Unique Identifier for Plant Area</param>
        /// <param name="plantArea">The items to update</param>
        [Route("{id}")]
        [HttpPatch]
        public IHttpActionResult Patch(int id, [FromBody]Delta<PlantArea> plantArea)
        {
            if (plantArea == null) throw new ArgumentNullException("plantArea");
            
            // Get the instrument to update 
            var databasePlantArea = GetPlantArea(id);
            if (databasePlantArea == null)
            {
                return this.NotFound();
            }

            // TODO: Go off and do the patch (in the service)
            plantArea.Patch(databasePlantArea);
            //// TODO: Save the changed entity

            return this.StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Delete an existing piece of Equipment by Id.
        /// </summary>
        /// <param name="id">Unique Identifier for Equipment</param>
        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var plantArea = GetPlantArea(id);
            if (plantArea == null)
            {
                return this.NotFound();
            }

            // TODO: Perform delete (soft)
            ////DeleteInstrument(id);

            return this.StatusCode(HttpStatusCode.NoContent);
        }

        private static PlantArea GetPlantArea(int id)
        {
            PlantArea plantArea;
            using (var sr = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Content/ApiDummyData/plantAreas.json")))
            {
                plantArea = JsonConvert.DeserializeObject<List<PlantArea>>(sr.ReadToEnd()).FirstOrDefault(i => i.Id == id);
            }

            return plantArea;
        }

        private static IEnumerable<PlantArea> GetPlantAreas()
        {
            IEnumerable<PlantArea> plantAreas;
            using (var sr = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Content/ApiDummyData/plantAreas.json")))
            {
                plantAreas = JsonConvert.DeserializeObject<List<PlantArea>>(sr.ReadToEnd());
            }

            return plantAreas;
        }
    }
}