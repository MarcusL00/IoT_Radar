using radar_frontend.Entities;
using Radar_Frontend.Models;

namespace radar_frontend.Interfaces
{
    public interface ISensorRepository
    {
        Task CreateAsync(List<SensorEntity> sensorEntities);
        Task<List<SensorEntity>> GetAsync(int skip, int take);
    }
}
