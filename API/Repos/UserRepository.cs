using Microsoft.Extensions.Options;
using SquishFaceAPI.Common;
using SquishFaceAPI.Model;
using SquishFaceAPI.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SquishFaceAPI.Repos
{
    public class UserRepository : RepositoryBase<User>, IRepository<User>
    {
        private readonly string _collection = string.Empty;

        public UserRepository(IOptions<AppConfig> appConfigs) : base(appConfigs)
        {
            _collection = AppCollections.Users.ToString();
        }

        public void CreateItem(User item)
        {
            MongoDBStorage<User>.CreateItem(_collection, item);
        }

        public async Task<IEnumerable<User>> GetAllItemsAsync()
        {
            return await MongoDBStorage<User>.GetAllItemsAsync(_collection);
        }

        public IEnumerable<User> GetAllItems()
        {
            return GetAllItemsAsync().Result;
        }

        public async Task<IEnumerable<User>> GetItemsAsync(Expression<Func<User, bool>> predicate)
        {
            return await MongoDBStorage<User>.GetItemsAsync(_collection, predicate);
        }

        public IEnumerable<User> GetItems(Expression<Func<User, bool>> predicate)
        {
            return GetItemsAsync(predicate).Result;
        }

        public async Task<User> GetItemAsync(Expression<Func<User, bool>> predicate)
        {
            return await MongoDBStorage<User>.GetItemAsync(_collection, predicate);
        }

        public User GetItem(Expression<Func<User, bool>> predicate)
        {
            return GetItemAsync(predicate).Result;
        }

        public void UpdateItem(Expression<Func<User, bool>> predicate, User item)
        {
            MongoDBStorage<User>.UpdateItem(_collection, predicate, item);
        }

        public Task<bool> RemoveItemAsync(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
