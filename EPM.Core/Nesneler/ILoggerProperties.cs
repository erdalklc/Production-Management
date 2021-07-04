using EPM.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Core.Nesneler
{
    public interface ILoggerProperties
    {
        public int CHANGED_BY { get; set; }
        public DateTime CHANGED_DATE { get; set; }
    }
}
