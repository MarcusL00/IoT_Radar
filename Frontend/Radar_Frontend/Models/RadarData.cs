
namespace Radar_Frontend.Models
{
    public class RadarData
    {
         internal int Rotation;
        internal List<SensorData> SensorDataList;
        internal string RadarId;
        internal DateTime Timestamp;


        public RadarData(int rotation, List<SensorData> sensorDataList, string radarId, DateTime timestamp)
        {
            Rotation = rotation;
            SensorDataList = sensorDataList;
            RadarId = radarId;
            Timestamp = timestamp;
        }
    }
}

