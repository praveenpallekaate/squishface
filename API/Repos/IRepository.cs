using SquishFaceAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SquishFaceAPI.Repos
{
    public interface IRepository<T> where T : IMongoDbModel
    {
        T GetItem(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetItems(Expression<Func<T, bool>> predicate);
        void CreateItem(T item);
        void UpdateItem(Expression<Func<T, bool>> predicate, T item);
        IEnumerable<T> GetAllItems();
        Task<bool> RemoveItemAsync(Expression<Func<T, bool>> predicate);
    }
}
