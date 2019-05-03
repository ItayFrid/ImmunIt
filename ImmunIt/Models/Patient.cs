using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ImmunIt.Models
{
    public class Patient : User
    {

        [ForeignKey("ImmuneCard")]
        public ImmuneCard card { get; set; }

        public string BloodType { get; set; }
        public string MedicineAllergy { get; set; }
        public string ChronicDiseases { get; set; }

    }
}