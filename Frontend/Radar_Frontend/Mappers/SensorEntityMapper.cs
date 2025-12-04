using radar_frontend.Entities;
using Radar_Frontend.Models;

namespace radar_frontend.Mappers
{
    internal static class SensorEntityMapper
    {
        internal static List<SensorEntity> RadarDataToSensorEntities(RadarData radarData)
        {
            List<SensorEntity> sensorEntities = new List<SensorEntity>();

            if (radarData.SensorData1 != null)
            {
                SensorEntity sensor1 = new SensorEntity
                {
                    // let MongoDB generate _id
                    RadarId = radarData.RadarId,
                    DistanceMeasured = radarData.SensorData1.DistanceMeasured,
                    Unit = radarData.SensorData1.Unit,
                    Rotation = radarData.Rotation,
                    Timestamp = radarData.Timestamp,
                };
                sensorEntities.Add(sensor1);
            }

            if (radarData.SensorData2 != null)
            {
                SensorEntity sensor2 = new SensorEntity
                {
                    // let MongoDB generate _id
                    RadarId = radarData.RadarId,
                    DistanceMeasured = radarData.SensorData2.DistanceMeasured,
                    Unit = radarData.SensorData2.Unit,
                    Rotation = radarData.Rotation,
                    Timestamp = radarData.Timestamp,
                };
                sensorEntities.Add(sensor2);
            }

            return sensorEntities;
        }
    }
}
