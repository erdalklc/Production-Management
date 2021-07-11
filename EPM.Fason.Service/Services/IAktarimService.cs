
using EPM.Fason.Dto.Fason;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Fason.Service.Services
{
    public interface IAktarimService
    {
        public object[] FasonUretimInsert(PRODUCTION_HEADER header);
         

        public List<PRODUCTION_PROCESS> GetProcessList();

        public List<PRODUCTION_FASON_USERS> GetFasonUsers();

        public object[] SiparisOlustur(PRODUCTION_HEADER header, List<PRODUCTION_PROCESS> plan, int firmaBilgi, DateTime terminTarihi);

        public List<PRODUCTION_STATUS> GetProductionStatus(int[] ids);

    }
}
