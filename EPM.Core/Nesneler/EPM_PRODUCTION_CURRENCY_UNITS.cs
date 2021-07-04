using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Core.Nesneler
{
    [Table("FDEIT005.EPM_PRODUCTION_CURRENCY_UNITS")]
    public class EPM_PRODUCTION_CURRENCY_UNITS
    {
        public int ID { get; set; }
        private string _CURRENCY_UNIT { get; set; }
        public string CURRENCY_UNIT
        {
            get
            {
                switch (_CURRENCY_UNIT)
                {
                    case "TL":
                        return "₺";
                    case "DOLAR":
                        return "$";
                    case "EURO":
                        return "€";
                }
                return _CURRENCY_UNIT;
            }
            set
            {
                _CURRENCY_UNIT = value;
            }
        }
    }
}
