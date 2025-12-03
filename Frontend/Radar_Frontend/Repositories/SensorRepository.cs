using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using radar_frontend.Interfaces;
using radar_frontend.Models;
using Radar_Frontend.Models;

namespace radar_frontend.Repositories
{
    public class SensorRepository : ISensorRepository
    {
        private readonly IMongoCollection<SensorData> _collection;

        public SensorRepository(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _collection = database.GetCollection<SensorData>(settings.Value.CollectionName);
        }

        public async Task CreateAsync(List<RadarData> radarData)
        {
            if (radarData == null || radarData.Count == 0)
                throw new ArgumentException("Radar data list cannot be null or empty.");

            await _collection.InsertManyAsync(radarData);
        }

        public async Task<List<SensorData>> GetAsync(int skip, int take)
        {
            return await _collection.Find(_ => true).Skip(skip).Limit(take).ToListAsync();
        }
    }
}
