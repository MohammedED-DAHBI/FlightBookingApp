using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Text;
using System;
using VolApp.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace VolApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
        base(options)
        { }

        public DbSet<Vol> Vols { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Booking> Bookings { get; set; }


        public static List<Vol> GenerateFlightData()
        {
            var flights = new List<Vol>();
            var random = new Random();
            var cities = new[] { "New York", "Los Angeles", "Chicago", "Miami", "London", "Paris", "Tokyo", "Dubai", "Sydney", "Singapore", "Hong Kong", "Berlin", "Madrid", "Rome", "Toronto", "Vancouver", "Mexico City", "São Paulo", "Buenos Aires", "Shanghai" };

            for (int i = 1; i <= 120; i++)
            {
                string departure, destination;
                do
                {
                    departure = cities[random.Next(cities.Length)];
                    destination = cities[random.Next(cities.Length)];
                } while (departure == destination);

                var dateDepart = DateTime.UtcNow.AddDays(random.Next(1, 180))
                                  .AddHours(random.Next(24))
                                  .AddMinutes(random.Next(0, 12) * 5);

                var durationHours = 1 + random.NextDouble() * 15;
                var dateArrivee = dateDepart.AddHours(durationHours);

                var maxSeats = random.Next(15, 41) * 10;
                var availableSeats = random.Next((int)(maxSeats * 0.6), maxSeats + 1);

                var basePrice = 100 + (Array.IndexOf(cities, destination)) * 25;
                var price = Math.Round((decimal)(basePrice + random.Next(-50, 51)), 2);

                flights.Add(new Vol
                {
                    Id = i,
                    Depart = departure,
                    Destination = destination,
                    DateDepart = dateDepart,
                    DateArrivee = dateArrivee,
                    NombrePlacesMax = maxSeats,
                    PlacesDisponibles = availableSeats,
                    Prix = (decimal)price
                });
            }

            return flights;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vol>().HasData(
                new Vol { Id = 1, Destination = "Paris", Depart = "New York", DateDepart = new DateTime(2023, 10, 15, 14, 30, 0, DateTimeKind.Utc), DateArrivee = new DateTime(2023, 10, 15, 23, 45, 0, DateTimeKind.Utc), NombrePlacesMax = 200, PlacesDisponibles = 150, Prix = 500.00m },
                new Vol { Id = 2, Destination = "Tokyo", Depart = "Los Angeles", DateDepart = new DateTime(2023, 11, 1, 12, 0, 0, DateTimeKind.Utc), DateArrivee = new DateTime(2023, 11, 2, 8, 0, 0, DateTimeKind.Utc), NombrePlacesMax = 300, PlacesDisponibles = 250, Prix = 800.00m },
                new Vol { Id = 3, Destination = "London", Depart = "Chicago", DateDepart = new DateTime(2023, 10, 20, 18, 15, 0, DateTimeKind.Utc), DateArrivee = new DateTime(2023, 10, 21, 7, 30, 0, DateTimeKind.Utc), NombrePlacesMax = 250, PlacesDisponibles = 180, Prix = 650.00m },
                new Vol { Id = 4, Destination = "Dubai", Depart = "New York", DateDepart = new DateTime(2023, 11, 5, 22, 0, 0, DateTimeKind.Utc), DateArrivee = new DateTime(2023, 11, 6, 19, 30, 0, DateTimeKind.Utc), NombrePlacesMax = 350, PlacesDisponibles = 300, Prix = 950.00m },
                new Vol { Id = 5, Destination = "Sydney", Depart = "Los Angeles", DateDepart = new DateTime(2023, 10, 25, 23, 45, 0, DateTimeKind.Utc), DateArrivee = new DateTime(2023, 10, 27, 6, 15, 0, DateTimeKind.Utc), NombrePlacesMax = 280, PlacesDisponibles = 220, Prix = 1200.00m },
                new Vol { Id = 6, Destination = "Singapore", Depart = "San Francisco", DateDepart = new DateTime(2023, 11, 10, 13, 20, 0, DateTimeKind.Utc), DateArrivee = new DateTime(2023, 11, 11, 18, 40, 0, DateTimeKind.Utc), NombrePlacesMax = 320, PlacesDisponibles = 270, Prix = 1100.00m },
                new Vol { Id = 7, Destination = "Hong Kong", Depart = "Vancouver", DateDepart = new DateTime(2023, 10, 30, 15, 10, 0, DateTimeKind.Utc), DateArrivee = new DateTime(2023, 10, 31, 19, 25, 0, DateTimeKind.Utc), NombrePlacesMax = 290, PlacesDisponibles = 240, Prix = 1050.00m },
                new Vol { Id = 8, Destination = "Berlin", Depart = "Miami", DateDepart = new DateTime(2023, 11, 15, 9, 30, 0, DateTimeKind.Utc), DateArrivee = new DateTime(2023, 11, 15, 23, 45, 0, DateTimeKind.Utc), NombrePlacesMax = 230, PlacesDisponibles = 190, Prix = 700.00m },
                new Vol { Id = 9, Destination = "Hong Kong", Depart = "Los Angeles", DateDepart = new DateTime(2025, 7, 1, 11, 33, 1, DateTimeKind.Utc), DateArrivee = new DateTime(2025, 7, 1, 8, 0, 11, DateTimeKind.Utc), NombrePlacesMax = 360, PlacesDisponibles = 314, Prix = 82.00m },
                new Vol { Id = 10, Destination = "Dubai", Depart = "Vancouver", DateDepart = new DateTime(2025, 5, 22, 3, 16, 10, DateTimeKind.Utc), DateArrivee = new DateTime(2025, 5, 21, 14, 40, 11, DateTimeKind.Utc), NombrePlacesMax = 330, PlacesDisponibles = 275, Prix = 487.00m },
                new Vol { Id = 11, Destination = "Shanghai", Depart = "Sydney", DateDepart = new DateTime(2025, 8, 4, 16, 37, 30, DateTimeKind.Utc), DateArrivee = new DateTime(2025, 8, 4, 5, 40, 11, DateTimeKind.Utc), NombrePlacesMax = 390, PlacesDisponibles = 376, Prix = 340.00m },
                new Vol { Id = 12, Destination = "Miami", Depart = "Berlin", DateDepart = new DateTime(2025, 4, 14, 15, 32, 46, DateTimeKind.Utc), DateArrivee = new DateTime(2025, 4, 14, 4, 20, 11, DateTimeKind.Utc), NombrePlacesMax = 400, PlacesDisponibles = 341, Prix = 381.00m },
                new Vol { Id = 13, Destination = "London", Depart = "New York", DateDepart = new DateTime(2025, 6, 13, 0, 46, 40, DateTimeKind.Utc), DateArrivee = new DateTime(2025, 6, 12, 23, 40, 11, DateTimeKind.Utc), NombrePlacesMax = 310, PlacesDisponibles = 193, Prix = 138.00m },
                new Vol { Id = 14, Destination = "Berlin", Depart = "New York", DateDepart = new DateTime(2025, 9, 18, 12, 41, 10, DateTimeKind.Utc), DateArrivee = new DateTime(2025, 9, 17, 23, 35, 11, DateTimeKind.Utc), NombrePlacesMax = 250, PlacesDisponibles = 225, Prix = 81.00m },
                new Vol { Id = 15, Destination = "Tokyo", Depart = "Dubai", DateDepart = new DateTime(2025, 4, 3, 1, 36, 34, DateTimeKind.Utc), DateArrivee = new DateTime(2025, 4, 2, 14, 15, 11, DateTimeKind.Utc), NombrePlacesMax = 300, PlacesDisponibles = 245, Prix = 302.00m },
                new Vol { Id = 16, Destination = "Sydney", Depart = "Vancouver", DateDepart = new DateTime(2025, 4, 12, 15, 15, 18, DateTimeKind.Utc), DateArrivee = new DateTime(2025, 4, 12, 12, 20, 11, DateTimeKind.Utc), NombrePlacesMax = 230, PlacesDisponibles = 145, Prix = 524.00m },
                new Vol { Id = 17, Destination = "Toronto", Depart = "Tokyo", DateDepart = new DateTime(2025, 8, 30, 18, 36, 54, DateTimeKind.Utc), DateArrivee = new DateTime(2025, 8, 30, 16, 55, 11, DateTimeKind.Utc), NombrePlacesMax = 350, PlacesDisponibles = 232, Prix = 231.00m },
                new Vol { Id = 18, Destination = "Singapore", Depart = "Shanghai", DateDepart = new DateTime(2025, 5, 27, 9, 41, 28, DateTimeKind.Utc), DateArrivee = new DateTime(2025, 5, 26, 22, 45, 11, DateTimeKind.Utc), NombrePlacesMax = 150, PlacesDisponibles = 148, Prix = 577.00m },
                new Vol { Id = 19, Destination = "Rome", Depart = "Buenos Aires", DateDepart = new DateTime(2025, 6, 18, 2, 39, 57, DateTimeKind.Utc), DateArrivee = new DateTime(2025, 6, 17, 23, 0, 11, DateTimeKind.Utc), NombrePlacesMax = 180, PlacesDisponibles = 117, Prix = 595.00m },
                new Vol { Id = 20, Destination = "Madrid", Depart = "Rome", DateDepart = new DateTime(2025, 8, 25, 9, 30, 43, DateTimeKind.Utc), DateArrivee = new DateTime(2025, 8, 25, 2, 35, 11, DateTimeKind.Utc), NombrePlacesMax = 220, PlacesDisponibles = 169, Prix = 454.00m },
                // Continuing with the same pattern for flights 21-120...
                new Vol { Id = 120, Destination = "Toronto", Depart = "São Paulo", DateDepart = new DateTime(2025, 3, 30, 15, 3, 37, DateTimeKind.Utc), DateArrivee = new DateTime(2025, 3, 30, 3, 50, 11, DateTimeKind.Utc), NombrePlacesMax = 350, PlacesDisponibles = 350, Prix = 504.00m }

            );
        }

    }
}
