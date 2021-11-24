using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace EPM.Tools.Helpers
{
    public class RedisCacheService : ICacheService
    {
        private RedisServer _redisServer;
        public RedisCacheService(RedisServer redisServer)
        {
            _redisServer = redisServer;
        }
        public void Add(int dbId, string key, object data,bool compress =false)
        {
            RedisServer.SetDatabase(dbId);
            string jsonData = JsonConvert.SerializeObject(data);
            if (compress)
                _redisServer.Database.StringSet(key, Compress(jsonData));
           else _redisServer.Database.StringSet(key, jsonData);
        }

        public void AddWithLifeTime(int dbId, string key, object data, TimeSpan lifeTime, bool compress = false)
        {
            RedisServer.SetDatabase(dbId);
            string jsonData = JsonConvert.SerializeObject(data);
            if (compress)
                _redisServer.Database.StringSet(key, Compress(jsonData), lifeTime);
            else _redisServer.Database.StringSet(key, jsonData, lifeTime);
        }

        public bool Any(int dbId, string key)
        {
            RedisServer.SetDatabase(dbId);
            return _redisServer.Database.KeyExists(key);
        }
        public List<string> GetAllKey(int dbId, string key)
        {
            RedisServer.SetDatabase(dbId);
            List<string> resultKeys = new List<string>();
            var keys = _redisServer.GetAllKeys(key);
            foreach (var item in keys)
            {
                resultKeys.Add(item.ToString());
            }
            return resultKeys;
        }
        public void Clear()
        {
            _redisServer.FlushDatabsae();
        }
        public T Get<T>(int dbId, string key, bool decompress = false)
        {
            RedisServer.SetDatabase(dbId);
            if (Any(dbId, key))
            {
                string jsonData = _redisServer.Database.StringGet(key);
                if (decompress)
                    return JsonConvert.DeserializeObject<T>(Decompress(jsonData));
                else return JsonConvert.DeserializeObject<T>(jsonData);
            }
            return default;
        }
        public void Remove(int dbId, string key)
        {
            RedisServer.SetDatabase(dbId);
            _redisServer.Database.KeyDelete(key);
        }
        private string Compress(string s)
        {
            var bytes = Encoding.Unicode.GetBytes(s);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    msi.CopyTo(gs);
                }
                return Convert.ToBase64String(mso.ToArray());
            }
        }
        private string Decompress(string s)
        {
            var bytes = Convert.FromBase64String(s);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    gs.CopyTo(mso);
                }
                return Encoding.Unicode.GetString(mso.ToArray());
            }
        }


    }
}