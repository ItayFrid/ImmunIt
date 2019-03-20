using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImmunIt.Models
{
    public class Vaccine
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime DateGiven { get; set; }
        public DateTime DateExpired { get; set; }
        [Required]
        public string MedicLicense { get; set; }
    }
}