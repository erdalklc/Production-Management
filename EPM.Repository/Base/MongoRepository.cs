using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Repository.Base
{
    public class MongoRepository : IMongoRepository
    {
        public string ConnectionString = "mongodb://10.1.1.159:27017";
        public string DatabaseName = "EPM";
        public IMongoCollection<T> GetList<T>(string CollectionName)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase(DatabaseName);
            CreateCollectionIfNotExits(CollectionName);
            return database.GetCollection<T>(CollectionName);
        }

        public void CreateCollectionIfNotExits(string CollectionName)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase(DatabaseName);
            var filter = new BsonDocument("name", CollectionName);
            var collections = database.ListCollections(new ListCollectionsOptions { Filter = filter });
            if (!collections.Any())
                database.CreateCollection(CollectionName);
        }
         
        public T Get<T>(string CollectionName, FilterDefinition<T> def)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase(DatabaseName);

            MongoDatabaseSettings set = new MongoDatabaseSettings();
            return database.GetCollection<T>(CollectionName).Find(def).FirstOrDefault();
        }
        public void Add<T>(string CollectionName, T item, IMongoCollection<T> collection)
        {
            collection.InsertOne(item);
        }
        public void AddList<T>(string CollectionName, List<T> item)
        {

            IMongoCollection<T> collection = GetList<T>(CollectionName);
            collection.InsertMany(item);
        }

        public void RemoveAll<T>(string CollectionName)
        {
            IMongoCollection<T> collection = GetList<T>(CollectionName);
            collection.DeleteMany(new BsonDocument());

        }

    }
}
