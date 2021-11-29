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
    [Table("bikes")]
    public class Bike
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        
        [MaxLength(50)]
        [Required]
        public string Model { get; set; }

        public int? Price { get; set; }

        
        [ForeignKey(nameof(Brand))]
        public int BrandID { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual Brand Brand { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Rental> Rentals { get; set; }

        public Bike()
        {
            Rentals = new HashSet<Rental>();
        }
        public override string ToString()
        {
            return $"{ID}. {Model}, Price: {Price}, Brand ID: {BrandID}";
        }
    }
}
