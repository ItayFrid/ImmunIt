using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImmunIt.Models
{
    public class VaccineJson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ExpDate { get; set; }
    }
}