namespace EOS2.WebAPI.Controllers.V1
{
    using System.Collections.Generic;
    using System.Web.Http;

    // [Authorize]
    public class ValuesV1Controller : ApiController
    {
        protected const int Version = 1;

        // GET api/values
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "No!")]
        [VersionedRoute("api/values", Version)]
        [Route("api/v1/values")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1.0", "value1.1" };
        }

        // GET api/values/5
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "No!"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "id", Justification = "Parameters required.")]
        [VersionedRoute("api/values/{id}", Version)]
        [Route("api/v1/values/{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "No!"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "value", Justification = "No!")]
        [VersionedRoute("api/values", Version)]
        [Route("api/v1/values")]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "No!"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "id", Justification = "No!"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "value", Justification = "No!")]
        [VersionedRoute("api/values/{id}", Version)]
        [Route("api/v1/values/{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "No!"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "id", Justification = "No!")]
        [VersionedRoute("api/values/{id}", Version)]
        [Route("api/v1/values/{id}")]
        public void Delete(int id)
        {
        }
    }
}
