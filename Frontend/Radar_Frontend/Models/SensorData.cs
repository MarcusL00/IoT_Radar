

using System.Text.Json.Serialization;

namespace Radar_Frontend.Models
{
    public class SensorData
    {
        [JsonPropertyName("distance")]
        public int DistanceMeasured { get; set; }
        [JsonPropertyName("unit")]
        public string Unit { get; set; } = string.Empty;
    }
}