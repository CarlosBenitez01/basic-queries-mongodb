using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Unab.Practice.Employees.Persistence.Contexts
{
    internal sealed class MongoContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly string _databaseName;
        public MongoContext(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _connectionString = configuration.GetConnectionString("Default") ?? throw new ArgumentNullException(nameof(configuration));
            _databaseName = configuration.GetSection("MongoDb")["database"]!;
        }
        internal IMongoDatabase GetDatabase()
        {
            var client = new MongoClient(_connectionString);
            return client.GetDatabase(_databaseName);
        }
    }
}
