
using System.Text.Json.Serialization;

namespace Radar_Frontend.Models
{
    public class RadarData
    {
        [JsonPropertyName("rotation")]
        internal int Rotation;
        [JsonPropertyName("sensor_1")]
        internal SensorData SensorData1;
        [JsonPropertyName("sensor_2")]
        internal SensorData SensorData2;
        [JsonPropertyName("radar_id")]
        internal string RadarId;
        [JsonPropertyName("timestamp")]
        internal DateTime Timestamp;



        public RadarData(int rotation, SensorData sensorData1, SensorData sensorData2, string radarId, DateTime timestamp)
        {
            Rotation = rotation;
            SensorData1 = sensorData1;
            SensorData2 = sensorData2;
            RadarId = radarId;
            Timestamp = timestamp;
        }
    }
}

