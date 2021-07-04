using EPM.Core.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Core.FormModels.UretimTakip
{
    public class SurecBilgileri
    {
        public int ID { get; set; }
        public int PO_HEADER_ID { get; set; }
        public int PROCESS_ID { get; set; }
        public DateTime START_DATE { get; set; }
        public DateTime END_DATE { get; set; }
        public int STATUS { get; set; }
        public string STATUS_TEXT { get { return Extension.GetDescription((SURECDURUMLARI)STATUS); } set { STATUS_TEXT = value; } }
        public string COLOR
        {
            get
            {
                if (STATUS == (int)SURECDURUMLARI.BASLADI)
                {
                    if (END_DATE > DateTime.Now.Date)
                    {
                        return "lawngreen";
                    }
                    else if (END_DATE == DateTime.Now.Date)
                    {
                        return "Orange";
                    }
                    else
                    {
                        return "Red";
                    }

                }
                else if (STATUS == (int)SURECDURUMLARI.TAMAMLANDI)
                {
                    return "Yellow";
                }
                else if (STATUS == (int)SURECDURUMLARI.BEKLEMEDE)
                {
                    return "LightGray";
                }
                else if (STATUS == (int)SURECDURUMLARI.EKSIKTAMAMLANAN)
                {
                    return "yellowgreen";
                }

                else return "White";
            }
            set { COLOR = value; }
        }
        public string PROCESS_NAME { get; set; }
    }
}
