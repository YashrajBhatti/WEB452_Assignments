using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcBike.Data;
using System;
using System.Linq;

namespace MvcBike.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MvcBikeContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MvcBikeContext>>()))
            {
                // Look for any Bikes.
                if (context.Bike.Any())
                {
                    return;   // DB has been seeded
                }

                context.Bike.AddRange(
                    new Bike
                    {
                        Model = "When Harry Met Sally",
                        LaunchDate = DateTime.Parse("1989-2-12"),
                        Company = "Romantic Comedy",
                        Price = 7.99M,
                        CC = 150,
                        Rating = "8.5"
                    },

                    new Bike
                    {
                        Model = "Ghostbusters ",
                        LaunchDate = DateTime.Parse("1984-3-13"),
                        Company = "Comedy",
                        Price = 8.99M,
                        CC = 150,
                        Rating = "8.5"
                    },

                    new Bike
                    {
                        Model = "Ghostbusters 2",
                        LaunchDate = DateTime.Parse("1986-2-23"),
                        Company = "Comedy",
                        Price = 9.99M,
                        CC = 150,
                        Rating = "8.5"
                    },

                    new Bike
                    {
                        Model = "Rio Bravo",
                        LaunchDate = DateTime.Parse("1959-4-15"),
                        Company = "Western",
                        Price = 3.99M,
                        CC = 150,
                        Rating = "8"
                    },
                    new Bike
                    {
                        Model = "Rio Bravo",
                        LaunchDate = DateTime.Parse("1959-4-15"),
                        Company = "Western",
                        Price = 3.99M,
                        CC = 150,
                        Rating = "8"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}