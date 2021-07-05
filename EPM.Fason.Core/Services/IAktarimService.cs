using EPM.Fason.Core.Models;
using EPM.Fason.Core.Nesneler;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Fason.Core.Services
{
    public interface IAktarimService
    {
        public object[] FasonUretimInsert(PRODUCTION_HEADER header);

        public PRODUCTION_STATUS GetProductionStatus(int HEADER_ID);

        public List<PRODUCTION_PROCESS> GetProcessList();

        public List<PRODUCTION_FASON_USERS> GetFasonUsers();

    }
}
