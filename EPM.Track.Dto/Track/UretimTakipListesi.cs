using EPM.Dto.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Track.Dto.Track
{
    public class UretimTakipListesi: EPM_MASTER_PRODUCTION_H
    {
        public int QUANTITY { get; set; }
        public int BANT_ADET { get; set; }
        public int KALITE_ADET { get; set; }
        public int KESIM_ADET { get; set; }
        public int TASNIF_ADET { get; set; }
        public DateTime END_DATE { get; set; }
        public string LAST_STATE { get; set; }
        public string PROCESS_INFO { get; set; }
        public string MUSTBE_STATE { get; set; }
        public string COMPANY_NAME { get; set; }
        public bool SATIN_ALMA_BAGLANTI { get; set; }
        public string TAKIP_NO { get; set; }
        private string _NEW_TAKIP_NO;
        public string NEW_TAKIP_NO {
            get {
                if (_NEW_TAKIP_NO == null)
                    _NEW_TAKIP_NO = DEADLINE.Year.ToString() + "." + MODEL + "-" + COLOR;
                return _NEW_TAKIP_NO;
            }
            set { _NEW_TAKIP_NO = value; }
        }
        private string _BACKGROUND_COLOR;
        public string BACKGROUND_COLOR
        {
            get
            {
                if (PRODUCTION_TYPE == 1)
                {
                    if (_BACKGROUND_COLOR == null)
                    {
                        if (SATIN_ALMA_BAGLANTI)
                        {
                            if (PROCESS_INFO == "TAMAMLANDI" || PROCESS_INFO == "TAKIP BASLATILMADI")
                                _BACKGROUND_COLOR = "lawngreen";
                            else if (PROCESS_INFO == "YOK")
                                _BACKGROUND_COLOR = "yellow";
                            else
                            {
                                if (END_DATE < DateTime.Now.Date)
                                    _BACKGROUND_COLOR = "IndianRed";
                                else if (END_DATE == DateTime.Now.Date)
                                    _BACKGROUND_COLOR = "Orange";
                                else
                                    _BACKGROUND_COLOR = "lawngreen";
                            }
                        }
                        else
                            _BACKGROUND_COLOR = "aqua";
                    }
                }
                else
                {
                    _BACKGROUND_COLOR = "white";
                }
               
                return _BACKGROUND_COLOR; 
            }
            set
            {
                _BACKGROUND_COLOR = value;

            }
        }
    }
}
