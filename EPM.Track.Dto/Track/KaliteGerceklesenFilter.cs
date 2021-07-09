using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EPM.Track.Dto.Track
{
    public class KaliteGerceklesenFilter
    {
        [Display(Name = "MARKA")]
        public int BRAND { get; set; } = 0;
        [Display(Name = "MODEL")]
        public string MODEL { get; set; } = "";
        [Display(Name = "RENK")]
        public string COLOR { get; set; } = "";
        [Display(Name = "BEDEN")]
        public string BEDEN { get; set; } = "";
        [Display(Name = "SEZON")]
        public int SEASON { get; set; } = 1; 
    }
}
