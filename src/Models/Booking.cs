using VolApp.Models;
using System.ComponentModel.DataAnnotations;

namespace VolApp.Models
{
    public enum BookingStatus
    {
        Pending,
        Confirmed,
        Canceled
    }

    public class Booking
    {
        [Key]
        public int Id { get; set; } // Unique booking ID

        [Required]
        public string UserId { get; set; } // ID of the user who made the booking

        [Required]
        public int VolId { get; set; } // ID of the flight being booked

        [Required]
        public int NumberOfSeats { get; set; } // Number of seats booked

        [Required]
        public BookingStatus Status { get; set; } = BookingStatus.Pending;


        // Navigation properties (optional, for EF Core relationships)
        public ApplicationUser User { get; set; }
        public Vol Vol { get; set; }
    }
}