using System; 
using System.ComponentModel.DataAnnotations; 

namespace EPM.Track.Dto.Track
{
    public class TrackList_Filter
    {
        [Display(Name = "BRAND")]
        public int BRAND { get; set; } = 0;
        [Display(Name = "MODEL")]
        public string MODEL { get; set; } = "";
        [Display(Name = "COLOR")]
        public string COLOR { get; set; } = "";
        [Display(Name = "SEASON")]
        public int SEASON { get; set; } = 1;
        [Display(Name = "FABRIC TYPE")]
        public int FABRIC_TYPE { get; set; } = 0;
        [Display(Name = "PRODUCTION TYPE")]
        public int PRODUCTION_TYPE { get; set; } = 0;
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

        public TrackList_Filter()
        {

        }
        public TrackList_Filter(int TYPE = 1)
        {
            if (TYPE == 1)
            {
                PRODUCTION_TYPE = 1;
                RECIPE = 1;
            }
            else if (TYPE == 2)
            {
                PRODUCTION_TYPE = 2;
                RECIPE = 5;
            }

            BRAND = 1;
        }
    }
}
