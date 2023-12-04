using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcBike.Models;

namespace MvcBike.Data
{
    public class MvcBikeContext : DbContext
    {
        public MvcBikeContext (DbContextOptions<MvcBikeContext> options)
            : base(options)
        {
        }

        public DbSet<MvcBike.Models.Bike> Bike { get; set; }
    }
}
