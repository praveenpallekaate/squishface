namespace SquishFaceAPI.Model
{
    public class AppConfig
    {
        public string Environment { get; set; }
        public DatabaseSettings Data { get; set; }
    }

    public class DatabaseSettings
    {
        public EnvironmentDatabaseSettings DEV { get; set; }
        public EnvironmentDatabaseSettings UAT { get; set; }
        public EnvironmentDatabaseSettings PROD { get; set; }
    }

    public class EnvironmentDatabaseSettings
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string AuthType { get; set; }
        public string Database { get; set; }
        public string Server { get; set; }
    }
}
