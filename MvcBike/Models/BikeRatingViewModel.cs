using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MvcBike.Models
{

    public class BikeRatingViewModel
    {
        public List<Bike>? Bikes { get; set; }
        public SelectList? Rating { get; set; }
        public string? bikeRating { get; set; }
        public string? SearchString { get; set; }
    }
}