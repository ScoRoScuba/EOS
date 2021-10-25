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

    [RoutePrefix("api/v1/Customer")]
    public class OrganizationController : ApiController
    {
        /// <summary>
        /// Gets you a list of Customers.
        /// </summary>
        [Route("")]
        [EnableQuery]
        public static IEnumerable<Organization> Get()
        {
            var customers = GetCustomers();

            return customers;
        }

        /// <summary>
        /// Gets an indiviual Customer by Id
        /// </summary>
        /// <param name="id">Unique Identifier for Customer</param>
        [Route("{id}")]
        [ResponseType(typeof(Organization))]
        public IHttpActionResult Get(int id)
        {
            var customer = GetCustomer(id);

            if (customer == null) return this.NotFound();

            return this.Ok(customer);
        }

        /// <summary>
        /// Create a new Customer
        /// </summary>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(Organization))]
        public IHttpActionResult Post([FromBody]Organization customer)
        {
            if (customer == null) throw new ArgumentNullException("customer");

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            // TODO: The save here (just setting a dummy Id of the created instrument for now)
            customer.Id = 999;

            return this.CreatedAtRoute("DefaultApi", new { controller = "organization", Id = customer.Id }, customer);
        }

        /// <summary>
        /// Replace an existing Customer.
        /// </summary>
        /// <param name="id">Unique Identifier for Customer</param>
        /// <param name="customer">The Customer to store</param>
        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]Organization customer)
        {
            if (customer == null) throw new ArgumentNullException("customer");

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            if (id != customer.Id)
            {
                return this.BadRequest("Customer Id mismatch");
            }

            var databaseCustomer = GetCustomer(id);
            if (databaseCustomer == null)
            {
                return this.NotFound();
            }

            // TODO: Make the save
            ////Save(equipment);

            return this.StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Update an existing Customer.
        /// </summary>
        /// <param name="id">Unique Identifier for Customer</param>
        /// <param name="customer">The items to update</param>
        [Route("{id}")]
        [HttpPatch]
        public IHttpActionResult Patch(int id, [FromBody]Delta<Organization> customer)
        {
            if (customer == null) throw new ArgumentNullException("customer");

            // Get the instrument to update 
            var databaseCustomer = GetCustomer(id);
            if (databaseCustomer == null)
            {
                return this.NotFound();
            }

            // TODO: Go off and do the patch (in the service)
            customer.Patch(databaseCustomer);
            //// TODO: Save the changed entity

            return this.StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Delete an existing Customer by Id.
        /// </summary>
        /// <param name="id">Unique Identifier for Customer</param>
        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var site = GetCustomer(id);
            if (site == null)
            {
                return this.NotFound();
            }

            // TODO: Perform delete (soft)
            ////DeleteInstrument(id);

            return this.StatusCode(HttpStatusCode.NoContent);
        }

        private static Organization GetCustomer(int id)
        {
            Organization customer;
            using (var sr = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Content/ApiDummyData/customers.json")))
            {
                customer = JsonConvert.DeserializeObject<List<Organization>>(sr.ReadToEnd()).FirstOrDefault(i => i.Id == id);
            }

            return customer;
        }

        private static IEnumerable<Organization> GetCustomers()
        {
            IEnumerable<Organization> customers;
            using (var sr = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Content/ApiDummyData/customers.json")))
            {
                customers = JsonConvert.DeserializeObject<List<Organization>>(sr.ReadToEnd());
            }

            return customers;
        }
    }
}