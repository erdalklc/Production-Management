using EPM.Core.Loglar;
using EPM.Core.Managers;
using EPM.Core.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Core.Services
{
    public class LogService :ILogService
    {
        public IMongoCollection<LOG_Login> _logs;
        public IMongoCollection<LOG_EPM_MASTER_PRODUCTION_H> _epmHLogs;
        public IMongoCollection<LOG_EPM_MASTER_PRODUCTION_D> _epmDLogs;

        public LogService()
        {
            _logs = MongoServer.GetList<LOG_Login>("LOG_Login");
            _epmHLogs = MongoServer.GetList<LOG_EPM_MASTER_PRODUCTION_H>("LOG_EPM_MASTER_PRODUCTION_H");
            _epmDLogs = MongoServer.GetList<LOG_EPM_MASTER_PRODUCTION_D>("LOG_EPM_MASTER_PRODUCTION_D");
        }
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
