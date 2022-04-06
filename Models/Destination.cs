using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Airport.Models
{
    [Table("Destination")]
    public class Destination
    {
        [Required]
        [Key]
        public int destinationId { get; set; }
        [Required]
        [Display(Name = "Град дестинация")]
        public string city { get; set; }
        [Required]
        [Display(Name = "Държава дестинация")]
        public string country { get; set; }
        [Required]
        [Display(Name = "Разстояние в км.")]
        public int distance { get; set; }

        //public virtual ICollection<Ticket> Ticket { get; set; }
        //public virtual ICollection<Schedule> Schedule { get; set; }
    }
}