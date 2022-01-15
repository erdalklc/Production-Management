
using EPM.Dto.Loglar;
using EPM.Repository.Base;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Service.Base
{
    public class LogService :ILogService
    {
        private readonly IMongoRepository _mongoRepository;
        public LogService(IMongoRepository mongoRepository)
        {
            _mongoRepository = mongoRepository;
            _logs = _mongoRepository.GetList<LOG_Login>("LOG_Login");
            _epmHLogs = _mongoRepository.GetList<LOG_EPM_MASTER_PRODUCTION_H>("LOG_EPM_MASTER_PRODUCTION_H");
            _epmDLogs = _mongoRepository.GetList<LOG_EPM_MASTER_PRODUCTION_D>("LOG_EPM_MASTER_PRODUCTION_D");
        }
        public IMongoCollection<LOG_Login> _logs;
        public IMongoCollection<LOG_EPM_MASTER_PRODUCTION_H> _epmHLogs;
        public IMongoCollection<LOG_EPM_MASTER_PRODUCTION_D> _epmDLogs;
         
        public void SaveDetail(LOG_EPM_MASTER_PRODUCTION_D t)
        {
            _epmDLogs.InsertOne(t);
        }
        public void SaveMaster(LOG_EPM_MASTER_PRODUCTION_H t)
        {
            _epmHLogs.InsertOne(t);
        }
        public void SaveLogin(LOG_Login t)
        {
            _logs.InsertOne(t);
        }

        public IEnumerable<LOG_EPM_MASTER_PRODUCTION_H> GetMaster()
        {
            return _epmHLogs.Find(_ => true).ToList();
        }
         
        public IEnumerable<LOG_EPM_MASTER_PRODUCTION_D> GetDetail()
        {
            return _epmDLogs.Find(_ => true).ToList();
        }

        public IEnumerable<LOG_Login> GetLogin()
        {
            return _logs.Find(_ => true).ToList();
        }
    }
}
