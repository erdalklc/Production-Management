using EPM.Fason.Core.Nesneler;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Fason.Core.Services
{
    public interface IAktarimService
    {
        public object[] FasonUretimInsert(PRODUCTION_HEADER header);
    }
}
