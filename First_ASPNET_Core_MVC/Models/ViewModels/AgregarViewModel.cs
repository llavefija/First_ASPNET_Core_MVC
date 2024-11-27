using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace First_ASPNET_Core_MVC.Models.ViewModels
{
    public class AgregarViewModel
    {
        [Required(ErrorMessage = "El nombre del animal es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string NombreAnimal { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "La raza del animal es obligatoria.")]
        [StringLength(50, ErrorMessage = "La raza no puede superar los 50 caracteres.")]
        public string Raza { get; set; }

        [Required(ErrorMessage = "Por favor seleccione un tipo de animal.")]
        public int SelectedTipoAnimalId { get; set; }

        // Lista para mostrar los tipos de animales en el ComboBox.
        public List<TipoAnimal>? TiposDeAnimales { get; set; }
    }
}
