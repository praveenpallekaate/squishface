using Microsoft.Extensions.Options;
using SquishFaceAPI.Common;
using SquishFaceAPI.Model;

namespace SquishFaceAPI.Helper
{
    public static class MongoHelper
    {
        public static string UserName { get; set; }
        public static string Password { get; set; }
        public static string AuthType { get; set; }
        public static string Database { get; set; }
        public static string Server { get; set; }
        public static dynamic Environment { get; set; }

        public static void SetDatabaseDetailsFromConfig(IOptions<AppConfig> appConfigs)
        {
            AppConfig appConfig = appConfigs.Value;

            UserName = UserName ?? (
                appConfig.Environment == Constants.ConfigPROD
                ? appConfig.Data.PROD.UserName
                : (
                    appConfig.Environment == Constants.ConfigUAT
                    ? appConfig.Data.UAT.UserName
                    : appConfig.Data.DEV.UserName
                   )
                );
            Password = Password ?? (
                appConfig.Environment == Constants.ConfigPROD
                ? appConfig.Data.PROD.Password
                : (
                    appConfig.Environment == Constants.ConfigUAT
                    ? appConfig.Data.UAT.Password
                    : appConfig.Data.DEV.Password
                   )
                );
            AuthType = AuthType ?? (
                appConfig.Environment == Constants.ConfigPROD
                ? appConfig.Data.PROD.AuthType
                : (
                    appConfig.Environment == Constants.ConfigUAT
                    ? appConfig.Data.UAT.AuthType
                    : appConfig.Data.DEV.AuthType
                   )
                );
            Database = Database ?? (
                appConfig.Environment == Constants.ConfigPROD
                ? appConfig.Data.PROD.Database
                : (
                    appConfig.Environment == Constants.ConfigUAT
                    ? appConfig.Data.UAT.Database
                    : appConfig.Data.DEV.Database
                   )
                );
            Server = Server ?? (
                appConfig.Environment == Constants.ConfigPROD
                ? appConfig.Data.PROD.Server
                : (
                    appConfig.Environment == Constants.ConfigUAT
                    ? appConfig.Data.UAT.Server
                    : appConfig.Data.DEV.Server
                   )
                );
        }
    }
}
