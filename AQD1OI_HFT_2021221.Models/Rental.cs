using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AQD1OI_HFT_2021221.Models
{
    [Table("rental")]
    public class Rental
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string Renter { get; set; }          //name of the person who rents the bike

        [ForeignKey(nameof(Bike))]
        public int BikeID { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [NotMapped]
        public virtual Bike Bike { get; set; }
    }
}
