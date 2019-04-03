using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ImmunIt.Models
{
    public class Vaccine
    {
        public Vaccine()
        {
            this.ImmunCards = new List<ImmunCard>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Vaccine Name must be between 3-50 characters")]
        public string Name { get; set; }
        public DateTime DateGiven { get; set; }
        public DateTime DateExpired { get; set; }
        [Required]
        public string MedicLicense { get; set; }
        public virtual ICollection<ImmunCard> ImmunCards { get; set; }
    }
}