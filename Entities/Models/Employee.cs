using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class Employee
    {
        [Column("EmployeeId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "De naam van de werknemer is verplicht.")]
        [MaxLength(30, ErrorMessage = "De maximale lengte van de naam is 30.")]
        public string Name { get; set; }
        [Required]
        [Range(18,67,ErrorMessage ="Leeftijd moet tussen 18 en 67 zijn")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Position is a required field.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the Position is 20 characters.")]
        public string Position { get; set; }
        [ForeignKey(nameof(Company))]
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
