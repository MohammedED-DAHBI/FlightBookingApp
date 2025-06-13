using System.ComponentModel.DataAnnotations;

namespace VolApp.Models
{
    public class CreateGestionnaireViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        // Gestionnaire-specific fields
        [Required]
        public string Nom { get; set; }

        public string Code { get; set; }

        public int? AnneeRecrutement { get; set; }

        public string Adresse { get; set; }

        public string CodePostal { get; set; }
    }
}