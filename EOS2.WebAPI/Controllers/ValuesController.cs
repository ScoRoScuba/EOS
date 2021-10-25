namespace EOS2.WebAPI.Controllers.V2
{
    using System.Collections.Generic;
    using System.Web.Http;

    // [Authorize]
    public class ValuesV2Controller : ApiController
    {
        protected const int Version = 2;

        // GET api/values
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "No!")]
        [VersionedRoute("api/values", Version)]
        [Route("api/v2/values")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value2.0", "value2.1" };
        }

        // GET api/values/5
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "No!"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "id", Justification = "Parameters required.")]
        [VersionedRoute("api/values/{id}", Version)]
        [Route("api/v2/values/{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "No!"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "value", Justification = "No!")]
        [VersionedRoute("api/values", Version)]
        [Route("api/v2/values")]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "No!"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "id", Justification = "No!"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "value", Justification = "No!")]
        [VersionedRoute("api/values/{id}", Version)]
        [Route("api/v2/values/{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "No!"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "id", Justification = "No!")]
        [VersionedRoute("api/values/{id}", Version)]
        [Route("api/v2/values/{id}")]
        public void Delete(int id)
        {
        }
    }
}
