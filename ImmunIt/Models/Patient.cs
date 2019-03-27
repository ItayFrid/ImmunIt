using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImmunIt.Models
{
    public class Patient
    {
        [Key]
        [Required]
        [RegularExpression("^[0-9]{9,9}$")]
        public string Id { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Name must be between 5-50")]
        public string Name { get; set; }
        public virtual ImmunCard ImmunCardId { get; set; }
        public string BloodType { get; set; }
        public string MedicineAllergy { get; set; }
        public string ChronicDiseases { get; set; }
        [Required]
        public string role { get; set; }
    }
}