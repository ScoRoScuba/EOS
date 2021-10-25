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

    [RoutePrefix("api/v1/instrument")]
    public class InstrumentController : ApiController
    {
        /// <summary>
        /// Gets you a list of Instruments.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "No!")]
        [Route("")]
        [EnableQuery]
        public IEnumerable<Instrument> Get()
        {
            var instruments = GetInstruments();

            return instruments;
        }

        /// <summary>
        /// Gets you a list of Instruments within a Plant Area.
        /// </summary>
        /// <param name="plantAreaId">Unique Identifier for Plant Area</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "No!")]
        [Route("~/api/v1/PlantArea/{plantAreaId}/Instrument")]
        [EnableQuery]
        public IEnumerable<Instrument> GetForPlantArea(int plantAreaId)
        {
            var instruments = GetInstruments().Where(i => i.PlantAreaId == plantAreaId);

            return instruments;
        }

        /// <summary>
        /// Gets an indiviual Instrument by Id
        /// </summary>
        /// <param name="id">Unique Identifier for Instrument</param>
        [Route("{id}")]
        [ResponseType(typeof(Instrument))]
        public IHttpActionResult Get(int id)
        {
            var instrument = GetInstrument(id);

            if (instrument == null) return this.NotFound();

            return this.Ok(instrument);
        }

        /// <summary>
        /// Create a new Instrument
        /// </summary>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(Instrument))]
        public IHttpActionResult Post([FromBody]Instrument instrument)
        {
            if (instrument == null) throw new ArgumentNullException("instrument");

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            // TODO: The save here (just setting a dummy Id of the created instrument for now)
            instrument.Id = 999;

            return this.CreatedAtRoute("DefaultApi", new { controller = "instrument", Id = instrument.Id }, instrument);
        }

        /// <summary>
        /// Replace an existing Instrument.
        /// </summary>
        /// <param name="id">Unique Identifier for Instrument</param>
        /// <param name="instrument">The instrument to store</param>
        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]Instrument instrument)
        {
            if (instrument == null) throw new ArgumentNullException("instrument");

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            if (id != instrument.Id)
            {
                return this.BadRequest("Instrument Id mismatch");
            }

            var dbinstrument = GetInstrument(id);
            if (dbinstrument == null)
            {
                return this.NotFound();
            }            

            // TODO: Make the save
            ////Save(instrument);
            
            return this.StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Update an existing Instrument.
        /// </summary>
        /// <param name="id">Unique Identifier for Instrument</param>
        /// <param name="instrument">The items to update</param>
        [Route("{id}")]
        [HttpPatch]
        public IHttpActionResult Patch(int id, [FromBody]Delta<Instrument> instrument)
        {
            if (instrument == null) throw new ArgumentNullException("instrument");

            // Get the instrument to update 
            var databaseInstrument = GetInstrument(id);
            if (databaseInstrument == null)
            {
                return this.NotFound();
            } 

            // TODO: Go off and do the patch (in the service)
            instrument.Patch(databaseInstrument);
            //// TODO: Save the changed entity

            return this.StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Delete an existing Instrument by Id.
        /// </summary>
        /// <param name="id">Unique Identifier for Instrument</param>
        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var instrument = GetInstrument(id);
            if (instrument == null)
            {
                return this.NotFound();
            }

            // TODO: Perform delete (soft)
            ////DeleteInstrument(id);

            return this.StatusCode(HttpStatusCode.NoContent);
        }

        private static Instrument GetInstrument(int id)
        {
            Instrument instrument;
            using (var sr = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Content/ApiDummyData/instruments.json")))
            {
                instrument = JsonConvert.DeserializeObject<List<Instrument>>(sr.ReadToEnd()).FirstOrDefault(i => i.Id == id);
            }

            return instrument;
        }

        private static IEnumerable<Instrument> GetInstruments()
        {
            IEnumerable<Instrument> instruments;
            using (var sr = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Content/ApiDummyData/instruments.json")))
            {
                instruments = JsonConvert.DeserializeObject<List<Instrument>>(sr.ReadToEnd());
            }

            return instruments;            
        }
    }
}