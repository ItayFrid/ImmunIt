using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImmunIt.Models
{
    public class Medic
    {
        [Key]
        [Required]
        public string UserName { get; set; }
        [Required]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "License Number must be  5 characters")]
        public string LicenseNumber { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be 5 to 50 characters")]
        public string Name { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Password must be 3 to 15 characters")]
        public string Password { get; set; }
        [Required]
        public string role { get; set; }
        [Required]
        public Boolean Permission { get; set; }
    }
}