namespace EOS2.WebAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Event", Justification = "Event is the most meaningful term (at present) and as API is required to be meaningful")]
    public class Event
    {
        /// <summary>
        /// The Unique Identifier of the Event
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The Name of the Event
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// The planned start date for the Event
        /// </summary>
        public DateTime EventDate { get; set; }

        /// <summary>
        /// The details of the User who raised the Event
        /// </summary>
        public User RaisedByUser { get; set; }

        /// <summary>
        /// The Date the Event was raised
        /// </summary>
        public DateTime RaisedDate { get; set; }

        /// <summary>
        /// The details of the Main Engineer for the Event
        /// </summary>
        public User MainEngineer { get; set; }

        /// <summary>
        /// The collection of Additional Engineers for the Event
        /// </summary>
        public IEnumerable<User> AdditionalEngineers { get; set; }

        /// <summary>
        /// The Unique Identifier of the Status of the Event
        /// </summary>
        public int StatusId { get; set; }

        /// <summary>
        /// The Date of the last change to the Status of the Event
        /// </summary>
        public DateTime StatusChangedDate { get; set; }

        /// <summary>
        /// The details of the User that last changed the Status of the Event
        /// </summary>
        public User StatusChangedUser { get; set; }

        /// <summary>
        /// The Unique Identifier of the Issued Format of the Event
        /// </summary>
        public int IssuedFormatId { get; set; }
    }
}