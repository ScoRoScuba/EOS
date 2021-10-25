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

    [RoutePrefix("api/v1/Event")]
    public class EventController : ApiController
    {
        /// <summary>
        /// Gets you a list of Events.
        /// </summary>
        [Route("")]
        [EnableQuery]
        public static IEnumerable<Event> Get()
        {
            var events = GetEvents();

            return events;
        }

        /// <summary>
        /// Gets an indiviual Event by Id
        /// </summary>
        /// <param name="id">Unique Identifier for an Event</param>
        [Route("{id}")]
        [ResponseType(typeof(Event))]
        public IHttpActionResult Get(int id)
        {
            var eevent = GetEvent(id);

            if (eevent == null) return this.NotFound();

            return this.Ok(eevent);
        }

        private static Event GetEvent(int id)
        {
            Event eevent;
            using (var sr = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Content/ApiDummyData/events.json")))
            {
                eevent = JsonConvert.DeserializeObject<List<Event>>(sr.ReadToEnd()).FirstOrDefault(i => i.Id == id);
            }

            return eevent;
        }

        private static IEnumerable<Event> GetEvents()
        {
            IEnumerable<Event> events;
            using (var sr = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Content/ApiDummyData/events.json")))
            {
                events = JsonConvert.DeserializeObject<List<Event>>(sr.ReadToEnd());
            }

            return events;
        }
    }
}
