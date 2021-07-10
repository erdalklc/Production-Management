using EPM.Track.Dto.Extensions;
using System;

namespace EPM.Track.Dto.Track
{
    public class SurecBilgileri
    {
        public int ID { get; set; }
        public int PO_HEADER_ID { get; set; }
        public int PROCESS_ID { get; set; }
        public DateTime START_DATE { get; set; }
        public DateTime END_DATE { get; set; }
        public int STATUS { get; set; }
        private string _STATUS_TEXT;
        public string STATUS_TEXT
        {
            get
            {
                if (_STATUS_TEXT == null) 
                    _STATUS_TEXT= TrackExtensions.GetDescription((SURECDURUMLARI)STATUS); 
                return _STATUS_TEXT;
            }
            set { _STATUS_TEXT = value; }
        } 
        private string _COLOR;
        public string COLOR
        {
            get
            {
                if (_COLOR == null)
                {
                    if (STATUS == (int)SURECDURUMLARI.BASLADI)
                    {
                        if (END_DATE > DateTime.Now.Date)
                        {
                            _COLOR= "lawngreen";
                        }
                        else if (END_DATE == DateTime.Now.Date)
                        {
                            _COLOR= "Orange";
                        }
                        else
                        {
                            _COLOR= "Red";
                        }

                    }
                    else if (STATUS == (int)SURECDURUMLARI.TAMAMLANDI)
                    {
                        _COLOR= "Yellow";
                    }
                    else if (STATUS == (int)SURECDURUMLARI.BEKLEMEDE)
                    {
                        _COLOR= "LightGray";
                    }
                    else if (STATUS == (int)SURECDURUMLARI.EKSIKTAMAMLANAN)
                    {
                        _COLOR= "yellowgreen";
                    }

                    else _COLOR= "White";
                }
                return _COLOR;
                
            }
            set { _COLOR = value; }
        }
        public string PROCESS_NAME { get; set; }
    }
}
