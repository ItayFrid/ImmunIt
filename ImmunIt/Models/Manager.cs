using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ImmunIt.Models
{
    public class Manager
    {
        [Key]
        [Required]
        public string UserName { get; set; }
        
        public string role { get; set; }
        [Required]
        public string Password { get; set; }

    }
}