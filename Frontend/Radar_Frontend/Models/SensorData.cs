

using System.Text.Json.Serialization;

namespace Radar_Frontend.Models
{
    public class SensorData
    {
        [JsonPropertyName("distance")]
        internal int DistanceMeasured;
        [JsonPropertyName("unit")]
        internal string Unit;


        public SensorData(int distanceMeasured, string unit)
        {
            DistanceMeasured = distanceMeasured;
            Unit = unit;
        }
    }
}