
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

        public object[] SiparisOlustur(CREATEORDER order);

        public List<PRODUCTION_STATUS> GetProductionStatus(int[] ids);

        public List<PRODUCTION_LIST_V> SurecDurumuGetir(int ENTEGRATION_ID);

    }
}
