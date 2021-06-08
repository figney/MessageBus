using Advertiser.Domain.Notifications;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Advertiser.Application.DomainServices
{
    public class NotificationDomainService
    {
        private readonly string _server;
        private readonly string _database;
        private readonly string _collection = "notifications";
        public NotificationDomainService(IConfiguration configuration)
        {
            _server = configuration["MongoDB:server"];
            _database = configuration["MongoDB:database"];
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

        public async Task<List<Notification>> GetNotificationsAsync(long userId)
        {
            var mongoClient = new MongoClient(_server);
            IMongoDatabase database = mongoClient.GetDatabase(_database);
            var collection = database.GetCollection<Notification>(_collection);
            IAsyncCursor<Notification> cursor = await collection.FindAsync(filter => filter.UserId == userId && filter.IsRead == false);
            return await cursor.ToListAsync();
        }

        public async Task<IChangeStreamCursor<ChangeStreamDocument<Notification>>> GetNotificationChangeStreamCursorAsync()
        {
            var mongoClient = new MongoClient(_server);
            IMongoDatabase database = mongoClient.GetDatabase(_database);
            var collection = database.GetCollection<Notification>(_collection);
            var options = new ChangeStreamOptions { FullDocument = ChangeStreamFullDocumentOption.UpdateLookup };
            var pipeline = new EmptyPipelineDefinition<ChangeStreamDocument<Notification>>().Match("{ operationType: { $in: [ 'insert' ] } }");
            IChangeStreamCursor<ChangeStreamDocument<Notification>> cursor = await collection.WatchAsync(pipeline, options);
            return cursor;
        }
    }
}
