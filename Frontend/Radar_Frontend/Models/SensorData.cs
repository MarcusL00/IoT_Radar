

namespace Radar_Frontend.Models
{
    public class SensorData
    {
        internal int DistanceMeasured;
        internal string Unit;


        public SensorData(int distanceMeasured, string unit)
        {
            DistanceMeasured = distanceMeasured;
            Unit = unit;
        }
    }
}