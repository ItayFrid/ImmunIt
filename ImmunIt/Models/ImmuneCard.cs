using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ImmunIt.Models
{
    public class ImmuneCard
    {
        public ImmuneCard()
        {
            this.Vaccines = new List<Vaccine>();
        }

        [Key]
        public string patientId { get; set; }
        

        public List<Vaccine> Vaccines { get; set; }

        
    }
}