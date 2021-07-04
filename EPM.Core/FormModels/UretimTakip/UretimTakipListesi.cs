using EPM.Core.Nesneler;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Core.FormModels.UretimTakip
{
    public class UretimTakipListesi: EPM_MASTER_PRODUCTION_H
    {
        public int QUANTITY { get; set; }
        public DateTime END_DATE { get; set; }
        public string LAST_STATE { get; set; }
        public string PROCESS_INFO { get; set; }
        public string MUSTBE_STATE { get; set; }
        public bool SATIN_ALMA_BAGLANTI { get; set; }
        public string TAKIP_NO { get; set; }
        public string BACKGROUND_COLOR
        {
            get
            {
                if (SATIN_ALMA_BAGLANTI)
                {
                    if (PROCESS_INFO == "TAMAMLANDI" || PROCESS_INFO == "TAKIP BASLATILMADI")
                    {
                        return "lawngreen";
                    }
                    else if (PROCESS_INFO == "YOK")
                    {

                        return "yellow";
                    }
                    else
                    {
                        if (END_DATE < DateTime.Now.Date)
                        {
                            return "IndianRed";
                        }
                        else if (END_DATE == DateTime.Now.Date)
                        {
                            return "Orange";
                        }
                        else
                        {
                            return "lawngreen";
                        }

                    }
                }
                else
                {
                    return "aqua";
                }


            }
            set
            {
                BACKGROUND_COLOR = value;

            }
        }
    }
}
