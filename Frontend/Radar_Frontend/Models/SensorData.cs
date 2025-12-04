

using System.Text.Json.Serialization;

namespace Radar_Frontend.Models
{
    public class SensorData
    {
        [JsonPropertyName("distance")]
        internal int DistanceMeasured;
        [JsonPropertyName("unit")]
        internal string Unit;
    }
}