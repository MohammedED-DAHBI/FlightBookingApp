using VolApp.Models;
using VolApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace VolApp.Controllers
{
    public class VolController : Controller
    {
        private readonly ApplicationDbContext _db;

        private readonly UserManager<IdentityUser> _userManager;

        public VolController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        // GET: Vol/Index
        public IActionResult Index()
        {
            List<Vol> volList = _db.Vols.ToList();
            ViewBag.Vols = volList;
            return View();
        }

        [Authorize(Roles = "Gestionnaire,Admin")]
        // GET: Vol/Create
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Gestionnaire,Admin")]
        // POST: Vol/Create
        [HttpPost]
        public IActionResult Create(Vol obj)
        {
            if (ModelState.IsValid)
            {
                _db.Vols.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        [Authorize(Roles = "Gestionnaire,Admin")]
        // GET: Vol/Edit/{id}
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Vol obj = _db.Vols.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [Authorize(Roles = "Gestionnaire,Admin")]
        // POST: Vol/Edit/{id}
        [HttpPost]
        public IActionResult Edit(Vol obj)
        {
            if (ModelState.IsValid)
            {
                _db.Vols.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        [Authorize(Roles = "Gestionnaire,Admin")]
        // GET: Vol/Delete/{id}
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Vol obj = _db.Vols.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [Authorize(Roles = "Gestionnaire,Admin")]
        // POST: Vol/Delete/{id}
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Vol obj = _db.Vols.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Vols.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Search action to handle the search request
        [HttpGet]
        public IActionResult Search(string depart, string destination, DateTime? dateDepart, DateTime? dateArrivee)
        {
            List<Vol> _vols = _db.Vols.ToList();
            // Start with all flights
            var results = _vols.AsQueryable();

            // Apply filters based on the provided search criteria
            if (!string.IsNullOrEmpty(depart))
            {
                results = results.Where(v => v.Depart.Contains(depart, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(destination))
            {
                results = results.Where(v => v.Destination.Contains(destination, StringComparison.OrdinalIgnoreCase));
            }

            if (dateDepart.HasValue)
            {
                results = results.Where(v => v.DateDepart.Date == dateDepart.Value.Date);
            }

            if (dateArrivee.HasValue)
            {
                results = results.Where(v => v.DateArrivee.Date == dateArrivee.Value.Date);
            }

            // Pass the search results to the view
            ViewBag.Vols = results.ToList();
            return View("Index");
        }


        // Action to display the booking page
        public IActionResult Book(int id)
        {
            List<Vol> volList = _db.Vols.ToList();
            var vol = volList.FirstOrDefault(v => v.Id == id);
            if (vol == null)
            {
                return NotFound();
            }
            return View(vol);
        }

        [Authorize(Roles = "Client")]
        // Action to handle the booking confirmation
        [HttpPost]
        public async Task<IActionResult> AddBooking(int id, int seats)
        {
            // Get the current user
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" }); // Redirect to login if the user is not authenticated
            }

            // Get the flight
            var vol = await _db.Vols.FindAsync(id);
            if (vol == null)
            {
                return NotFound();
            }

            if (seats <= 0 || seats > vol.PlacesDisponibles)
            {
                ModelState.AddModelError("seats", "Invalid number of seats.");
                return View("Book", vol);
            }

            // Create a new booking
            var booking = new Booking
            {
                UserId = user.Id,
                VolId = vol.Id,
                NumberOfSeats = seats,
                Status = BookingStatus.Pending // Default status
            };

            // Update the number of available seats
            vol.PlacesDisponibles -= seats;

            // Save changes to the database
            _db.Bookings.Add(booking);
            _db.Vols.Update(vol);
            await _db.SaveChangesAsync();

            // Redirect to the flight list with a success message
            TempData["Message"] = $"Waiting for confirmation for booking {seats} seat(s) on flight {vol.Depart} to {vol.Destination}.";
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Client")]
        // Fetch Bookings for current user
        public async Task<IActionResult> MyBookings(string statusFilter = "Pending",
    List<BookingStatus>? statuses = null)
        {
            // Get the current user
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            statuses ??= new List<BookingStatus> { BookingStatus.Pending, BookingStatus.Confirmed, BookingStatus.Canceled };
            // Fetch the user's bookings from the database
            var bookings = await _db.Bookings
                .Include(b => b.Vol) // Include the flight details
                .Where(b => b.UserId == user.Id)
                .Where(b => statuses.Contains(b.Status))
                .ToListAsync();

            ViewBag.SelectedStatuses = statuses;
            var bookingsResult = bookings.ToList();
            return View(bookingsResult);
        }

        [Authorize(Roles = "Client")]
        [HttpPost]
        public async Task<IActionResult> CancelBooking(int bookingId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            var booking = await _db.Bookings
                .Include(b => b.Vol)
                .FirstOrDefaultAsync(b => b.Id == bookingId && b.UserId == user.Id);

            if (booking == null)
            {
                return NotFound();
            }

            // Update the booking status to "canceled"
            booking.Status = BookingStatus.Canceled;

            // Restore the number of available seats for the flight
            booking.Vol.PlacesDisponibles += booking.NumberOfSeats;

            // Save changes to the database
            await _db.SaveChangesAsync();

            // Redirect to the MyBookings page with a success message
            TempData["Message"] = "Booking canceled successfully.";
            return RedirectToAction("MyBookings");
        }


        [Authorize(Roles = "Gestionnaire,Admin")]
        public async Task<IActionResult> PendingReservations(string statusFilter = "Pending",
    List<BookingStatus>? statuses = null)
        {
            statuses ??= new List<BookingStatus> { BookingStatus.Pending, BookingStatus.Confirmed, BookingStatus.Canceled };
            // Fetch all bookings
            var bookings = await _db.Bookings
                .Include(b => b.Vol)
                .Include(b => b.User)
                .Where(b => statuses.Contains(b.Status))
                .ToListAsync();

            // Filter pending reservations
            var all = await _db.Bookings
                .Include(b => b.Vol)
                .Include(b => b.User)
                .ToListAsync();
            var pendingReservations = all.ToList();
            // Calculate statistics
            var totalReservations = pendingReservations.Count;
            var totalCancellations = pendingReservations.Count(b => b.Status == BookingStatus.Canceled);
            var totalPending = pendingReservations.Count(b => b.Status == BookingStatus.Pending);
            var totalConfirmed = pendingReservations.Count(b => b.Status == BookingStatus.Confirmed);

            // Pass data to the view
            ViewBag.TotalReservations = totalReservations;
            ViewBag.TotalCancellations = totalCancellations;
            ViewBag.TotalPending = totalPending;
            ViewBag.TotalConfirmed = totalConfirmed;
            ViewBag.SelectedStatuses = statuses;
            //return View(bookings);
            pendingReservations = bookings.ToList();
            return View(pendingReservations);
        }


        [Authorize(Roles = "Gestionnaire,Admin")]
        [HttpPost]
        public async Task<IActionResult> ConfirmBooking(int bookingId)
        {
            var booking = await _db.Bookings
                .Include(b => b.Vol)
                .FirstOrDefaultAsync(b => b.Id == bookingId);

            if (booking == null)
            {
                return NotFound();
            }

            // Update the booking status to "confirmed"
            booking.Status = BookingStatus.Confirmed;

            // Save changes to the database
            await _db.SaveChangesAsync();

            // Redirect to the PendingReservations page with a success message
            TempData["Message"] = "Booking confirmed successfully.";
            return RedirectToAction("PendingReservations");
        }


        [Authorize(Roles = "Gestionnaire,Admin")]
        [HttpPost]
        public async Task<IActionResult> RefuseBooking(int bookingId)
        {
            var booking = await _db.Bookings
                .Include(b => b.Vol)
                .FirstOrDefaultAsync(b => b.Id == bookingId);

            if (booking == null)
            {
                return NotFound();
            }

            // Update the booking status to "canceled"
            booking.Status = BookingStatus.Canceled;

            // Restore the number of available seats for the flight
            booking.Vol.PlacesDisponibles += booking.NumberOfSeats;

            // Save changes to the database
            await _db.SaveChangesAsync();

            // Redirect to the PendingReservations page with a success message
            TempData["Message"] = "Booking canceled successfully.";
            return RedirectToAction("PendingReservations");
        }



    }
}