using Radar_Frontend.Models;

namespace radar_frontend.Interfaces
{
    public interface ISensorRepository
    {
        Task CreateAsync(List<RadarData> radarData);
        Task<List<SensorData>> GetAsync(int skip, int take);
    }
}
