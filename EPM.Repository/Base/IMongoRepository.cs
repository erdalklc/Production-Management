using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Repository.Base
{
    public interface IMongoRepository
    {
        public IMongoCollection<T> GetList<T>(string CollectionName);

        public void CreateCollectionIfNotExits(string CollectionName);

        public T Get<T>(string CollectionName, FilterDefinition<T> def);

        public void Add<T>(string CollectionName, T item, IMongoCollection<T> collection);

        public void AddList<T>(string CollectionName, List<T> item);

        public void RemoveAll<T>(string CollectionName);

    }
}
