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
        internal long Timestamp;
    }
}
