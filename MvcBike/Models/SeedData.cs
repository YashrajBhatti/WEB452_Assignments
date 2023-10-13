using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace MvcBike.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Check if there are any existing records; if there are, do not seed the data again
                if (context.Todo.Any())
                {
                    return; // Database has been seeded
                }

                // Seed data with car information
                var bike = new Todo[]
                {
                    new Todo
                    {
                        Model = "550",
                        Make = "BMW" ,
                        Price = 15000 ,
                        LaunchYear  = 2018,
                        CC = 540 ,
                        Mileage = 15
                    },
                    new Todo
                    {
                        Model = "b550",
                        Make = "BMW" ,
                        Price = 15000 ,
                        LaunchYear  = 2018,
                        CC = 540 ,
                        Mileage = 15
                    },
                    new Todo
                    {
                        Model = "650",
                        Make = "BMW" ,
                        Price = 15000 ,
                        LaunchYear  = 2018,
                        CC = 540 ,
                        Mileage = 15
                    },
                    new Todo
                    {
                        Model = "1000",
                        Make = "BMW" ,
                        Price = 15000 ,
                        LaunchYear  = 2018,
                        CC = 540 ,
                        Mileage = 15
                    },
                    new Todo
                    {
                        Model = "350",
                        Make = "BMW" ,
                        Price = 15000 ,
                        LaunchYear  = 2018,
                        CC = 540 ,
                        Mileage = 15
                    },
                    new Todo
                    {
                        Model = "Classic",
                        Make = "RE" ,
                        Price = 15000 ,
                        LaunchYear  = 2018,
                        CC = 540 ,
                        Mileage = 15
                    }
                };

                context.Todo.AddRange(bike);
                context.SaveChanges();
            }
        }
    }
}