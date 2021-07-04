using EPM.Core.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq; 
using System.Reflection;
using MongoDB.Bson;

namespace EPM.Core.Managers
{
    public class MongoServer
    {

        public static string ConnectionString = "mongodb://10.1.1.159:27017";
        public static string DatabaseName = "EPM";
        public static IMongoCollection<T> GetList<T>(string CollectionName)
        {
            var client = new MongoClient(ConnectionString); 
            var database = client.GetDatabase(DatabaseName);
            CreateCollectionIfNotExits(CollectionName);
            return database.GetCollection<T>(CollectionName);
        }

        public static void CreateCollectionIfNotExits(string CollectionName)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase(DatabaseName);
            var filter = new BsonDocument("name", CollectionName); 
            var collections = database.ListCollections(new ListCollectionsOptions { Filter = filter }); 
            if (!collections.Any()) 
                database.CreateCollection(CollectionName); 
        }

        public static void Test()
        { 
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase(DatabaseName); 
        }

        public static T Get<T>(string CollectionName, FilterDefinition<T> def)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase(DatabaseName); 

            MongoDatabaseSettings set = new MongoDatabaseSettings(); 
            return database.GetCollection<T>(CollectionName).Find(def).FirstOrDefault();
        }
        public static void Add<T>( string CollectionName,T item, IMongoCollection<T> collection)
        { 
            collection.InsertOne(item);
        }
        public static void AddList<T>(string CollectionName, List<T> item)
        {

            IMongoCollection<T> collection = GetList<T>(CollectionName);
            collection.InsertMany(item);
        }

        public static void RemoveAll<T>(string CollectionName)
        {
            IMongoCollection<T> collection = GetList<T>(CollectionName);
            collection.DeleteMany(new BsonDocument());

        }


    }
}
