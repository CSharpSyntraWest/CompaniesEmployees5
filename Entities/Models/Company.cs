using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Company")]
    public class Company
    {
        [Column("CompanyId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Naam is een verplicht veld.")]
        [MaxLength(60, ErrorMessage = "Maximum lengte voor de naam is 60.")]
        [Display(Name = "Naam")]
        public string Name { get; set; }
        [Required(ErrorMessage = "adres is een verplicht veld.")]
        [MaxLength(60, ErrorMessage = "Maximum lengte voor het adres is 60")]
        [Display(Name = "Adres")]
        public string Address { get; set; }
        [Display(Name = "Land")]
        public string Country { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
