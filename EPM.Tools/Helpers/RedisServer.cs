using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace EPM.Tools.Helpers
{
    public class RedisServer
    {
        private static ConnectionMultiplexer _connectionMultiplexer;
        private static IDatabase _database;
        private string _configurationString;
        private int _currentDatabaseId = 0;

        public RedisServer(IConfiguration configuration)
        {
            //_configurationString = "synctimeout=100000,10.1.1.178:6379,abortConnect=False";
            _configurationString = "synctimeout=60000,10.1.1.159:6379,password=hcGkGQPI91K7jAeofZFuOcDoU9B3n655,abortConnect=False";
            _connectionMultiplexer = ConnectionMultiplexer.Connect(_configurationString);
            //_database = _connectionMultiplexer.GetDatabase(_currentDatabaseId);
        }

        public static void SetDatabase(int db)
        {
            _database = _connectionMultiplexer.GetDatabase(db);
        }

        public IDatabase Database => _database;

        public void FlushDatabsae()
        {
            _connectionMultiplexer.GetServer(_configurationString).FlushDatabase(_currentDatabaseId);
        }

        public IEnumerable<RedisKey> GetAllKeys(string key)
        {
            var endpoints = _connectionMultiplexer.GetEndPoints();
            return _connectionMultiplexer.GetServer(endpoints[0]).Keys(0, pattern: $"*{key}*");
        }


        private void CreateRedisConfigurationString(IConfiguration configuration)
        {

        }
    }
}
