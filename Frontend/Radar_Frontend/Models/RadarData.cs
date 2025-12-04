
using System.Text.Json.Serialization;

namespace Radar_Frontend.Models
{
    public class RadarData
    {
        [JsonPropertyName("rotation")]
        public int Rotation { get; set; }
        [JsonPropertyName("sensor_1")]
        public SensorData? SensorData1 { get; set; }
        [JsonPropertyName("sensor_2")]
        public SensorData? SensorData2 { get; set; }
        [JsonPropertyName("radar_id")]
        public string RadarId { get; set; } = string.Empty;
        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }
    }
}

