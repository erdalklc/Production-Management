using EPM.Dto.Loglar;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Service.Base
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
