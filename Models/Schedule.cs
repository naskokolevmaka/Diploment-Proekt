using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Airport.Areas.Identity.Data;

namespace Airport.Models
{
    [Table("Schedule")]
    public class Schedule
    {
        [Key]
        [Display(Name = "Номер на полет")]
        public int flightId { get; set; }

        [ForeignKey("Airline")]
        [Display(Name = "Номер на авиокомпания")]
        public int airlineId { get; set; }

        [ForeignKey("Destination")]
        [Display(Name = "Номер на дестинация")]
        public int destinationId { get; set; }

        [Required]
        [Display(Name = "Цена на билет")]
        public float price { get; set; }

        [Required]
        [Display(Name = "Дата на полет")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime scheduleDate { get; set; }
      
        [Required]
        [Display(Name = "Час на излитане")]
        public TimeSpan departureTime { get; set; }


        public virtual Destination Destination { get; set; }
        public virtual Airline Airline { get; set; }
        public virtual ICollection<Ticket> Ticket { get; set; }
        public virtual ICollection<User> User { get; set; }


    }
}