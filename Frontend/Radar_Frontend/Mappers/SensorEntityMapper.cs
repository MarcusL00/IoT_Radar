using radar_frontend.Entities;
using Radar_Frontend.Models;

namespace radar_frontend.Mappers
{
    internal static class SensorEntityMapper
    {
        internal static List<SensorEntity> RadarDataToSensorEntities(RadarData radarData)
        {
            List<SensorEntity> sensorEntities = new List<SensorEntity>();

            SensorEntity sensor1 = new SensorEntity
            {
                //sensorId = 1, // Assuming sensor IDs are predefined
                Id = radarData.RadarId,
                DistanceMeasured = radarData.SensorData1.DistanceMeasured,
                Unit = radarData.SensorData1.Unit,
                Rotation = radarData.Rotation,
                Timestamp = radarData.Timestamp,
            };

            SensorEntity sensor2 = new SensorEntity
            {
                //sensorId = 2, // Assuming sensor IDs are predefined
                Id = radarData.RadarId,
                DistanceMeasured = radarData.SensorData2.DistanceMeasured,
                Unit = radarData.SensorData2.Unit,
                Rotation = radarData.Rotation,
                Timestamp = radarData.Timestamp,
            };

            sensorEntities.Add(sensor1);
            sensorEntities.Add(sensor2);

            return sensorEntities;
        }
    }
}
