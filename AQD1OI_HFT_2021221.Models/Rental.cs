using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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
        [JsonIgnore]
        public virtual Bike Bike { get; set; }
        public override string ToString()
        {
            return $"{ID}. Renter: {Renter} BikeId: {BikeID} Rental date: {Date}";
        }
    }
}
