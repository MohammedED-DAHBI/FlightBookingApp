using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace VolApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Nom { get; set; } // Common attribute for all users

        public string Adresse { get; set; } // Common attribute for all users
        public string CodePostal { get; set; } // Common attribute for all users

        // Role-specific attributes (nullable)
        public string CIN { get; set; } // For Clients
        public int? Age { get; set; } // For Clients (nullable)
        public string Code { get; set; } // For Gestionnaires
        public int? AnneeRecrutement { get; set; } // For Gestionnaires (nullable)

        // Role property (optional, if not using IdentityRole)
        public string Role { get; set; } // "Gestionnaire" or "Client"

        public ICollection<Booking> Bookings { get; set; }
    }
}