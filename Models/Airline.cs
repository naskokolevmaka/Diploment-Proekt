using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Airport.Models
{
    [Table("Airline")]
    public class Airline
    {
        [Required]
        [Key]
        public int airlineId { get; set; }
        [Required]
        [Display(Name = "Име на авиокомпания")]
        public string airlineName { get; set; }
        [Required]
        [Display(Name = "Кратко описание")]
        public string description { get; set; }

        public virtual ICollection<Schedule> Schedule { get; set; }
        public virtual Destination Destination { get; set; }
        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}