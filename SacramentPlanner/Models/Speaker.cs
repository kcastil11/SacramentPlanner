using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SacramentPlanner.Models
{
    public class Speaker
    {
        public int SpeakerID { get; set; }

        [Display(Name = "Sunday")]
        [DataType(DataType.Date)]
        public int SundayID { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public String Subject { get; set; }

        public Sunday Sunday { get; set; }
    }
}
