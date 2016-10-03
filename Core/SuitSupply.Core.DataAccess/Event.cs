using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SuitSupply.Core.Extensions;

namespace SuitSupply.Core.DataAccess
{
    [Table("SuitSupply.Event")]
    public class Event
    {
        public Event()
        {
            CreatedOn = DateTime.Now.ToUtcTime();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string CommandId { get; set; }

        [Required]
        public string Payload { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool WasCommandSuccessfull { get; set; }
    }
}