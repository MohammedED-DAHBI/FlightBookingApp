using VolApp.Models;
using VolApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace VolApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GestionnaireController : Controller
    {
        private readonly ApplicationDbContext _db;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public GestionnaireController(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: List all "gestionnaire" users
        public async Task<IActionResult> Index()
        {
            // Get users in role and cast to ApplicationUser
            var usersInRole = await _userManager.GetUsersInRoleAsync("Gestionnaire");
            var gestionnaires = usersInRole.Cast<ApplicationUser>().ToList();

            return View(gestionnaires);
        }

        // GET: Form to add a new "gestionnaire"
        public IActionResult Create()
        {
            return View(new CreateGestionnaireViewModel());
        }

        // POST: Add a new "gestionnaire"
        [HttpPost]
        public async Task<IActionResult> Create(CreateGestionnaireViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true,
                Nom = model.Nom,
                Code = model.Code,
                AnneeRecrutement = model.AnneeRecrutement,
                Adresse = model.Adresse,
                CodePostal = model.CodePostal,
                Role = "Gestionnaire" // Set the role property
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Ensure the role exists
                if (!await _roleManager.RoleExistsAsync("Gestionnaire"))
                    await _roleManager.CreateAsync(new IdentityRole("Gestionnaire"));

                // Assign the role
                await _userManager.AddToRoleAsync(user, "Gestionnaire");
                TempData["Success"] = "Gestionnaire created successfully!";
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(model);
        }

        // POST: Delete a "gestionnaire"
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                TempData["Error"] = "User not found.";
                return RedirectToAction("Index");
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
                TempData["Success"] = "User deleted successfully!";
            else
                TempData["Error"] = "Failed to delete user.";

            return RedirectToAction("Index");
        }
    }
}
