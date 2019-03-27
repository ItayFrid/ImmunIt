using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ImmunIt.Models
{
    public class ImmunCard
    {
        public ImmunCard()
        {
            this.Vaccines = new HashSet<Vaccine>();
        }
        [ForeignKey("Patient")] 
        [Required]
        public string Id { get; set; }
        public virtual Patient Patient { get; set; }
        public ICollection<Vaccine> Vaccines { get; set; }
    }
}