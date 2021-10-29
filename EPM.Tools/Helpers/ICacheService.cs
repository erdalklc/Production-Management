﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Tools.Helpers
{
    public interface ICacheService
    {
        T Get<T>(int dbId, string key);
        List<string> GetAllKey(int dbId, string key);
        void Add(int dbId, string key, object data);
        void AddWithLifeTime(int dbId, string key, object data,TimeSpan lifeTime);
        void Remove(int dbId, string key);
        void Clear();
        bool Any(int dbId, string key);
    }
}
