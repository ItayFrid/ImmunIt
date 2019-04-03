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
            this.Vaccines = new List<Vaccine>();
        }
        [ForeignKey("Patient")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
     
        public virtual ICollection<Vaccine> Vaccines { get; set; }
        public virtual Patient Patient { get; set; }
    }
}