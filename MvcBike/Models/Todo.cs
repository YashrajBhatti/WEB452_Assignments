using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MvcBike.Models
{
    public class Todo
    {
        public int Id { get; set; }

        [DisplayName("Make")]
        [Required(ErrorMessage = "Make is required")]
        public string Make { get; set; } = "";

        [DisplayName("Model")]
        public string? Model { get; set; }

        [DisplayName("Price")]
        public int Price { get; set; }

        [DisplayName("Launch Year")]
        public int LaunchYear { get; set; }

        public int CC { get; set; }
        public int Mileage { get; set; }
    }
}
