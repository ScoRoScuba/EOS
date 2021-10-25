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

    [RoutePrefix("api/v1/channel")]
    public class ChannelController : ApiController
    {
        /// <summary>
        /// Gets you a list of Channels.
        /// </summary>
        [Route("")]
        [EnableQuery]
        public static IEnumerable<Channel> Get()
        {
            var channels = GetChannels();

            return channels;
        }

        /// <summary>
        /// Gets you a list of Channels by InstrumentId.
        /// </summary>
        [Route("~/api/v1/Instrument/{instrumentId}/Channel")]
        [EnableQuery]
        public static IEnumerable<Channel> GetByInstrument(int instrumentId)
        {
            // TODO: Do this properly (i.e. so only channels for that instrument are got from db)
            var channels = GetChannels().Where(i => i.InstrumentId == instrumentId);

            return channels;
        }

        /// <summary>
        /// Gets you a list of Channels by EquipmentId.
        /// </summary>
        [Route("~/api/v1/Equipment/{equipmentId}/Channel")]
        [EnableQuery]
        public static IEnumerable<Channel> GetByEquipment(int equipmentId)
        {
            // TODO: Do this properly (i.e. so only channels for that instrument are got from db)
            var channels = GetChannels().Where(i => i.EquipmentId == equipmentId);

            return channels;
        }

        /// <summary>
        /// Gets an indiviual Channel by Id
        /// </summary>
        /// <param name="id">Unique Identifier for Channel</param>
        [Route("{id}")]
        [ResponseType(typeof(Channel))]
        public IHttpActionResult Get(int id)
        {
            var channel = GetChannel(id);

            if (channel == null) return this.NotFound();

            return this.Ok(channel);
        }

        /// <summary>
        /// Create a new Channel
        /// </summary>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(Channel))]
        public IHttpActionResult Post([FromBody]Channel channel)
        {
            if (channel == null) throw new ArgumentNullException("channel");

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            // TODO: The save here (just setting a dummy Id of the created instrument for now)
            channel.Id = 999;

            return this.CreatedAtRoute("DefaultApi", new { controller = "channel", Id = channel.Id }, channel);
        }

        /// <summary>
        /// Replace an existing Channel.
        /// </summary>
        /// <param name="id">Unique Identifier for Channel</param>
        /// <param name="channel">The Channel to store</param>
        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]Channel channel)
        {
            if (channel == null) throw new ArgumentNullException("channel"); 
            
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            if (id != channel.Id)
            {
                return this.BadRequest("Channel Id mismatch");
            }

            var dbchannel = GetChannel(id);
            if (dbchannel == null)
            {
                return this.NotFound();
            }

            // TODO: Make the save
            ////Save(instrument);

            return this.StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Update an existing Channel.
        /// </summary>
        /// <param name="id">Unique Identifier for Channel</param>
        /// <param name="channel">The items to update</param>
        [Route("{id}")]
        [HttpPatch]
        public IHttpActionResult Patch(int id, [FromBody]Delta<Channel> channel)
        {
            if (channel == null) throw new ArgumentNullException("channel");

            // Get the instrument to update 
            var databaseChannel = GetChannel(id);
            if (databaseChannel == null)
            {
                return this.NotFound();
            }

            // TODO: Go off and do the patch (in the service)
            channel.Patch(databaseChannel);
            //// TODO: Save the changed entity

            return this.StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Delete an existing Channel by Id.
        /// </summary>
        /// <param name="id">Unique Identifier for Channel</param>
        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var channel = GetChannel(id);
            if (channel == null)
            {
                return this.NotFound();
            }

            // TODO: Perform delete (soft)
            ////DeleteInstrument(id);

            return this.StatusCode(HttpStatusCode.NoContent);
        }

        private static Channel GetChannel(int id)
        {
            Channel channel;
            using (var sr = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Content/ApiDummyData/channels.json")))
            {
                channel = JsonConvert.DeserializeObject<List<Channel>>(sr.ReadToEnd()).FirstOrDefault(i => i.Id == id);
            }

            return channel;
        }

        private static IEnumerable<Channel> GetChannels()
        {
            IEnumerable<Channel> channels;
            using (var sr = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Content/ApiDummyData/channels.json")))
            {
                channels = JsonConvert.DeserializeObject<List<Channel>>(sr.ReadToEnd());
            }

            return channels;
        }
    }
}