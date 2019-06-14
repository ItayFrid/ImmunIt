using ImmunIt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImmunIt.ViewModels
{
    public class ViewModel
    {
        public ImmuneCard immuneCard { get; set; }
        public List<ImmuneCard> immuneCards { get; set; }
        public Medic medic { get; set; }
        public List<Medic> medics { get; set; }
        public Patient patient { get; set; }
        public List<Patient> patients { get; set; }
        public Manager manager { get; set; }
        public List<Manager> managers { get; set; }
        public Vaccine vaccine { get; set; }
        public List<Vaccine> vaccines { get; set; }
        public List<VaccineJson> vaccinesJson { get; set; }

        public List<User> users { get; set; }
    }
}