using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VolApp.Models
{
    public class Vol
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Destination")]
        [Required(ErrorMessage = "Veuillez saisir une destination")]
        [MaxLength(100)]
        public string Destination { get; set; }

        [DisplayName("Départ")]
        [Required(ErrorMessage = "Veuillez saisir un lieu de départ")]
        [MaxLength(100)]
        public string Depart { get; set; }

        [DisplayName("Date de départ")]
        [Required(ErrorMessage = "Veuillez saisir une date de départ")]
        public DateTime DateDepart { get; set; }

        [DisplayName("Date d'arrivée")]
        [Required(ErrorMessage = "Veuillez saisir une date d'arrivée")]
        public DateTime DateArrivee { get; set; }

        [DisplayName("Nombre de places maximum")]
        [Required(ErrorMessage = "Veuillez saisir le nombre de places maximum")]
        [Range(1, 1000, ErrorMessage = "Le nombre de places doit être compris entre 1 et 1000")]
        public int NombrePlacesMax { get; set; }

        [DisplayName("Places disponibles")]
        [Required(ErrorMessage = "Veuillez saisir le nombre de places disponibles")]
        [Range(0, 1000, ErrorMessage = "Le nombre de places disponibles doit être compris entre 0 et 1000")]
        public int PlacesDisponibles { get; set; }

        [DisplayName("Prix")]
        [Required(ErrorMessage = "Veuillez saisir un prix")]
        [Range(0.01, 1000000, ErrorMessage = "Le prix doit être compris entre 0.01 et 1,000,000")]
        public decimal Prix { get; set; }
    }
}