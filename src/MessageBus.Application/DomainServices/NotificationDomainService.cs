using MessageBus.Application.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace MessageBus.Application.DomainServices
{
    public class NotificationDomainService
    {
        private readonly string _server;
        private readonly string _database;
        private readonly string _collection = "";
        public NotificationDomainService(IConfiguration configuration)
        {
            _server = configuration["mongo:server"];
            _database = configuration["mongo:database"];
        }

        /// <summary>
        /// 删除没用的消息通知
        /// </summary>
        /// <returns></returns>
        public async Task RemoveUselessNoticesAsync()
        {
            var mongoClient = new MongoClient(_server);
            IMongoDatabase database = mongoClient.GetDatabase(_database);
            var collection = database.GetCollection<Notification>(_collection);
            await collection.DeleteManyAsync(filter => filter.CreationTime < DateTime.Now.AddDays(-5));
        }
    }
}
