using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MvcBike.Models
{

    public class BikeCompanyViewModel
    {
        public List<Bike>? Bikes { get; set; }
        public SelectList? Company { get; set; }
        public string? BikeCompany { get; set; }
        public string? SearchString { get; set; }
    }
}