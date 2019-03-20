using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImmunIt.Models
{
    public class ImmunCard
    {
        [Key]
        [Required]
        public string Id { get; set; }
        //Foreign Keys to Vaccine class
        public string Vaccines { get; set; }
    }
}