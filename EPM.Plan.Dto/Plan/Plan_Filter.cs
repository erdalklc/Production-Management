using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace EPM.Plan.Dto.Plan
{
    public class Plan_Filter
    {
        [Display(Name = "BRAND")]
        public int BRAND { get; set; } = 1;
        [Display(Name = "MODEL")]
        public string MODEL { get; set; } = "";
        [Display(Name = "COLOR")]
        public string COLOR { get; set; } = "";
        [Display(Name = "SEASON")]
        public int SEASON { get; set; } = 1;
        [Display(Name = "FABRIC TYPE")]
        public int FABRIC_TYPE { get; set; } = 0;
        [Display(Name = "PRODUCTION TYPE")]
        public int PRODUCTION_TYPE { get; set; } = 1;
        [Display(Name = "ORDER TYPE")]
        public int ORDER_TYPE { get; set; } = 0;
        [Display(Name = "DEADLINE")]
        public DateTime DEADLINE { get; set; } = DateTime.Now;
        [Display(Name = "RECIPE")]
        public int RECIPE { get; set; } = 0;
        [Display(Name = "BAND")]
        public int BAND_ID { get; set; } = 0;
        [Display(Name = "APPROVAL STATUS")]
        public int APPROVAL_STATUS { get; set; } = 0;
        [Display(Name = "PRODUCT GROUP")]
        public int PRODUCT_GROUP { get; set; } = 0;
        public int PLAN_DURUM { get; set; } = 0;

        public List<string> YEAR { get; set; }
    }
    
     
}
