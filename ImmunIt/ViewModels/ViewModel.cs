using ImmunIt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImmunIt.ViewModels
{
    public class ViewModel
    {
        public ImmunCard immunCard { get; set; }
        public List<ImmunCard> immunCards { get; set; }
        public Medic medic { get; set; }
        public List<Medic> medics { get; set; }
        public User user { get; set; }
        public List<User> users { get; set; }
    }
}