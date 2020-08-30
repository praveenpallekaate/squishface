using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SquishFaceAPI.Common;
using SquishFaceAPI.Model;
using SquishFaceAPI.Model.Data;
using System.Linq.Expressions;

namespace SquishFaceAPI.Repos
{
    public class MessageRepository : RepositoryBase<Message>, IRepository<Message>
    {
        private readonly string _collection = string.Empty;

        public MessageRepository(IOptions<AppConfig> appConfigs) : base(appConfigs)
        {
            _collection = AppCollections.Messages.ToString();
        }

        public void CreateItem(Message item)
        {
            if (item != null)
            {
                item.On = DateTime.Now;
            }

            MongoDBStorage<Message>.CreateItem(_collection, item);
        }

        public async Task<IEnumerable<Message>> GetAllItemsAsync()
        {
            return await MongoDBStorage<Message>.GetAllItemsAsync(_collection);
        }

        public IEnumerable<Message> GetAllItems()
        {
            return GetAllItemsAsync().Result;
        }

        public async Task<IEnumerable<Message>> GetItemsAsync(Expression<Func<Message, bool>> predicate)
        {
            return await MongoDBStorage<Message>.GetItemsAsync(_collection, predicate);
        }

        public IEnumerable<Message> GetItems(Expression<Func<Message, bool>> predicate)
        {
            return GetItemsAsync(predicate).Result;
        }

        public async Task<Message> GetItemAsync(Expression<Func<Message, bool>> predicate)
        {
            return await MongoDBStorage<Message>.GetItemAsync(_collection, predicate);
        }

        public Message GetItem(Expression<Func<Message, bool>> predicate)
        {
            return GetItemAsync(predicate).Result;
        }

        public void UpdateItem(Expression<Func<Message, bool>> predicate, Message item)
        {
            MongoDBStorage<Message>.UpdateItem(_collection, predicate, item);
        }

        public async Task<bool> RemoveItemAsync(Expression<Func<Message, bool>> predicate)
        {
            return await MongoDBStorage<Message>.DeleteAsync(_collection, predicate);
        }
    }
}
