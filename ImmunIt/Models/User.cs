using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImmunIt.Models
{
    public class User
    {
        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Name must be between 5-50")]
        public string Name { get; set; }

        [Key]
        [Required]
        [RegularExpression("^[0-9]{9,9}$")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Id Must be 9 digits!")]
        public string Id { get; set; }

        [Required]
        [StringLength(9999999,MinimumLength = 3, ErrorMessage = "Password must be at least 3 characters")]
        public string Password { get; set; }

        [Required]
        public string role { get; set; }

    }
}