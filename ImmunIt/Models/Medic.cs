using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImmunIt.Models
{
    public class Medic : User
    {

        [Required]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "License Number must be  5 characters")]
        public string LicenseNumber { get; set; }

    }
}