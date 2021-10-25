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

    /// <summary>
    /// Retrieve and maintain Equipment details. 
    /// Examples of Equipment - Furnace, Oven, Quench Bath 
    /// </summary>
    [RoutePrefix("api/v1/Equipment")]
    public class EquipmentController : ApiController
    {
        /// <summary>
        /// Gets you a list of Equipment.
        /// </summary>
        [Route("")]
        [EnableQuery]
        public static IEnumerable<Equipment> Get()
        {
            var equipment = GetEquipments();

            return equipment;
        }

        /// <summary>
        /// Gets you a list of Equipment within a Plant Area.
        /// </summary>
        /// <param name="plantAreaId">Unique Identifier for Plant Area</param>
        [Route("~/api/v1/PlantArea/{plantAreaId}/Equipment")]
        [EnableQuery]
        public static IEnumerable<Equipment> GetForPlantArea2(int plantAreaId)
        {
            var equipments = GetEquipments().Where(e => e.PlantAreaId == plantAreaId);

            return equipments;
        }

        /// <summary>
        /// Gets an indiviual piece of Equipment by Id
        /// </summary>
        /// <param name="id">Unique Identifier for Equipment</param>
        [Route("{id}")]
        [ResponseType(typeof(Equipment))]
        public IHttpActionResult Get(int id)
        {
            var equipment = GetEquipment(id);

            if (equipment == null) return this.NotFound();

            return this.Ok(equipment);
        }

        /// <summary>
        /// Create a new piece of Equipment
        /// </summary>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(Equipment))]
        public IHttpActionResult Post([FromBody]Equipment equipment)
        {
            if (equipment == null) throw new ArgumentNullException("equipment");

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            // TODO: The save here (just setting a dummy Id of the created instrument for now)
            equipment.Id = 999;

            return this.CreatedAtRoute("DefaultApi", new { controller = "equipment", Id = equipment.Id }, equipment);
        }

        /// <summary>
        /// Replace an existing piece of Equipment.
        /// </summary>
        /// <param name="id">Unique Identifier for Equipment</param>
        /// <param name="equipment">The Equipment to store</param>
        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]Equipment equipment)
        {
            if (equipment == null) throw new ArgumentNullException("equipment");

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            if (id != equipment.Id)
            {
                return this.BadRequest("Equipment Id mismatch");
            }

            var databaseEquipment = GetEquipment(id);
            if (databaseEquipment == null)
            {
                return this.NotFound();
            }

            // TODO: Make the save
            ////Save(equipment);

            return this.StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Update an existing piece of Equipment.
        /// </summary>
        /// <param name="id">Unique Identifier for Equipment</param>
        /// <param name="equipment">The items to update</param>
        [Route("{id}")]
        [HttpPatch]
        public IHttpActionResult Patch(int id, [FromBody]Delta<Equipment> equipment)
        {
            if (equipment == null) throw new ArgumentNullException("equipment");

            // Get the instrument to update 
            var databaseEquipment = GetEquipment(id);
            if (databaseEquipment == null)
            {
                return this.NotFound();
            }

            // TODO: Go off and do the patch (in the service)
            equipment.Patch(databaseEquipment);
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
            var equipment = GetEquipment(id);
            if (equipment == null)
            {
                return this.NotFound();
            }

            // TODO: Perform delete (soft)
            ////DeleteInstrument(id);

            return this.StatusCode(HttpStatusCode.NoContent);
        }

        private static Equipment GetEquipment(int id)
        {
            Equipment equipment;
            using (var sr = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Content/ApiDummyData/equipments.json")))
            {
                equipment = JsonConvert.DeserializeObject<List<Equipment>>(sr.ReadToEnd()).FirstOrDefault(i => i.Id == id);
            }

            return equipment;
        }

        private static IEnumerable<Equipment> GetEquipments()
        {
            IEnumerable<Equipment> equipments;
            using (var sr = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Content/ApiDummyData/equipments.json")))
            {
                equipments = JsonConvert.DeserializeObject<List<Equipment>>(sr.ReadToEnd());
            }

            return equipments;
        }
    }
}