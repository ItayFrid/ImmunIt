using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImmunIt.Models
{
    public class User
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
        public string ImmunCardId { get; set; }
        [Required]
        public string role { get; set; }
    }
}