using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Airport.Areas.Identity.Data;

namespace Airport.Models
{
    [Table("Ticket")]
    public class Ticket
    {
        [Key]
        public int ticketId { get; set; }

        [Required]
        [ForeignKey("UserId")]
        [Display(Name = "Номер на клиент")]
        public string UserId { get; set; }

        [Required]
        [ForeignKey("Schedule")]
        [Display(Name = "Номер на Полет")]
        public int flightId { get; set; }

        [Required]
        [Display(Name = "Брой билети")]
        public int ticketCount { get; set; }

        [Display(Name = "Дата на резервация")]
        [DataType(DataType.Date)]
        public DateTime dateOfJourney { get; set; }
        

        public virtual User User { get; set; }
        public virtual Schedule Schedule { get; set; }
        //public virtual ApplicationUser ApplicationUser{ get; set; }
    }
}
