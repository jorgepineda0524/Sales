﻿using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Sales.Shared.Entities
{
    public class State
    {
        public int Id { get; set; }

        [Display(Name = "Estaso/Departamento")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener mas de {1} caractéres.")]
        public string Name { get; set; } = null!;

        public Country? Country { get; set; }

        public ICollection<City>? Cities { get; set; }

        public int CitiesNumber => Cities == null ? 0 : Cities.Count;

    }
}