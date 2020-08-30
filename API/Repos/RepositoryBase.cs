using Microsoft.Extensions.Options;
using SquishFaceAPI.Common;
using SquishFaceAPI.Helper;
using SquishFaceAPI.Model;

namespace SquishFaceAPI.Repos
{
    public class RepositoryBase<T> where T : IMongoDbModel
    {
        public RepositoryBase(IOptions<AppConfig> appConfigs)
        {
            MongoHelper.SetDatabaseDetailsFromConfig(appConfigs);
            MongoDBStorage<T>.Initialize();
        }
    }
}
