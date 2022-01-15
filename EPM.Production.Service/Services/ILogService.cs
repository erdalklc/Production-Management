using EPM.Dto.Loglar;
using System.Collections.Generic;

namespace EPM.Production.Service.Services
{
    public interface ILogService
    {
        public void SaveDetail(LOG_EPM_MASTER_PRODUCTION_D t);
        public void SaveMaster(LOG_EPM_MASTER_PRODUCTION_H t);
        public void SaveLogin(LOG_Login t);
        public IEnumerable<LOG_EPM_MASTER_PRODUCTION_H> GetMaster(); 
        public IEnumerable<LOG_EPM_MASTER_PRODUCTION_D> GetDetail();
        public IEnumerable<LOG_Login> GetLogin();

    }
}
