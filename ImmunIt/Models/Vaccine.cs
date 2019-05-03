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
            this.ImmunCards = new List<ImmuneCard>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Vaccine Name must be between 3-50 characters")]
        public string Name { get; set; }
        public string description { get; set; }
        public DateTime DateGiven { get; set; }
        public DateTime DateExpired { get; set; }
        public virtual Medic Medic { get; set; }
        public virtual ICollection<ImmuneCard> ImmunCards { get; set; }

        //This method returns a red color if a vaccine has expired
        public string getExpiredColor()
        {
            if (this.DateExpired < DateTime.Now)
                return "text-danger";
            return "";
        }
    }
}