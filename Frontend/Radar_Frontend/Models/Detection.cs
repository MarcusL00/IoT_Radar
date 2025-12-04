using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radar_Frontend.Models
{
    public class Detection
    {
        public string Left { get; set; } = "0px";
		public string Top { get; set; } = "0px";
		public int SensorIndex { get; set; } = 0; // 0 = sensor1, 1 = sensor2
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}