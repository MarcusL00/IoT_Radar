using Microsoft.Extensions.Options;
using MongoDB.Driver;
using radar_frontend.Entities;
using radar_frontend.Interfaces;
using radar_frontend.Models;

namespace radar_frontend.Repositories
{
    public class SensorRepository : ISensorRepository
    {
        private readonly IMongoCollection<SensorEntity> _collection;

        public SensorRepository(IOptions<MongoDbSettings> settings)
        {
            var cfg = settings.Value;
            var client = new MongoClient(cfg.ConnectionString);
            var database = client.GetDatabase(cfg.DatabaseName);
            _collection = database.GetCollection<SensorEntity>(cfg.CollectionName);
        }

        public async Task CreateAsync(List<SensorEntity> sensorEntities)
        {
            if (sensorEntities == null || sensorEntities.Count == 0)
                throw new ArgumentException("Sensor entities list cannot be null or empty.");

            await _collection.InsertManyAsync(sensorEntities);
        }

        public async Task<List<SensorEntity>> GetAsync(int skip, int take)
        {
            return await _collection.Find(_ => true).Skip(skip).Limit(take).ToListAsync();
        }
    }
}
