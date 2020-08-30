using MongoDB.Driver;
using SquishFaceAPI.Helper;
using SquishFaceAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SquishFaceAPI.Common
{
    public static class MongoDBStorage<T> where T : IMongoDbModel
    {
        private static MongoClient mongoClient;
        private static IMongoDatabase mongoDatabase;
        private static readonly object syncLock = new object();

        #region async
        public static async Task CreateItemAsync(string collection, T item)
        {
            var mongoCollection = mongoDatabase.GetCollection<T>(collection);
            await mongoCollection.InsertOneAsync(item);
        }

        public static async Task UpdateItemAsync(string collection, Expression<Func<T, bool>> predicate, T item)
        {
            var mongoCollection = mongoDatabase.GetCollection<T>(collection);
            await mongoCollection.ReplaceOneAsync(predicate, item);
        }

        public static async Task<IEnumerable<T>> GetAllItemsAsync(string collection)
        {
            var mongoCollection = mongoDatabase.GetCollection<T>(collection);
            var result = await mongoCollection.AsQueryable().ToListAsync();
            return result;
        }

        public static async Task<IEnumerable<T>> GetItemsAsync(string collection, Expression<Func<T, bool>> predicate)
        {
            var mongoCollection = mongoDatabase.GetCollection<T>(collection);
            var result = (await mongoCollection.FindAsync(predicate)).ToList();
            return result;
        }

        public static async Task<T> GetItemAsync(string collection, Expression<Func<T, bool>> predicate)
        {
            var mongoCollection = mongoDatabase.GetCollection<T>(collection);
            var result = (await mongoCollection.FindAsync(predicate)).FirstOrDefault();
            return result;
        }

        public static async Task<bool> DeleteAsync(string collection, Expression<Func<T, bool>> predicate)
        {
            var mongoCollection = mongoDatabase.GetCollection<T>(collection);
            var result = (await mongoCollection.DeleteOneAsync(predicate));
            return result.IsAcknowledged;
        }
        #endregion

        #region sync
        public static void CreateItem(string collection, T item)
        {
            CreateItemAsync(collection, item).Wait();
        }

        public static void UpdateItem(string collection, Expression<Func<T, bool>> predicate, T item)
        {
            UpdateItemAsync(collection, predicate, item).Wait();
        }

        public static IEnumerable<T> GetAllItems(string collection)
        {
            return GetAllItemsAsync(collection).Result;
        }

        public static IEnumerable<T> GetItems(string collection, Expression<Func<T, bool>> predicate)
        {
            return GetItemsAsync(collection, predicate).Result;
        }

        public static T GetItem(string collection, Expression<Func<T, bool>> predicate)
        {
            return GetItemAsync(collection, predicate).Result;
        }
        #endregion

        public static void Initialize()
        {
            if (mongoClient == null)
            {
                lock (syncLock)
                {
                    if (mongoClient == null)
                    {
                        mongoClient = GetClient();
                    }
                }
            }

            if (mongoDatabase == null)
            {
                lock (syncLock)
                {
                    if (mongoDatabase == null)
                    {
                        mongoDatabase = mongoClient?.GetDatabase(MongoHelper.Database);
                    }
                }
            }
        }

        private static MongoClient GetClient()
        {
            MongoInternalIdentity internalIdentity =
                      new MongoInternalIdentity(MongoHelper.Database, MongoHelper.UserName);
            PasswordEvidence passwordEvidence = new PasswordEvidence(MongoHelper.Password);
            MongoCredential mongoCredential =
                 new MongoCredential(MongoHelper.AuthType,
                         internalIdentity, passwordEvidence);
            List<MongoCredential> credentials =
                       new List<MongoCredential>() { mongoCredential };


            MongoClientSettings settings = new MongoClientSettings
            {
                Credentials = credentials
            };
            String mongoHost = MongoHelper.Server;
            MongoServerAddress address = new MongoServerAddress(MongoHelper.Server);
            settings.Server = address;
            return new MongoClient(settings);
        }
    }
}
