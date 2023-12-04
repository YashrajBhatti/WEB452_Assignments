using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcBike.Models
{
    public class Bike
    {
        internal bool IsHidden;

        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string? Model { get; set; }

        [Display(Name = "Launch Date")]
        [DataType(DataType.Date)]
        public DateTime LaunchDate { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        [Required]
        [StringLength(30)]
        public string? Company { get; set; }

        [Range(1, 10000)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Required]
       [Range(1, 1000)]
        public int CC { get; set; }

        [Required]
        public string? Rating { get; set; }
        
    }
}