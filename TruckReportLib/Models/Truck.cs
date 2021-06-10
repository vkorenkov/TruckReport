using System;
using System.Collections.Generic;
using System.Text;

namespace TruckReportLib.Models
{
    public class Truck
    {
        public string TruckNumber { get; set; }

        public DateTime? MoveTime { get; set; }

        public DateTime? StopTime { get; set; }

        public float CurrentFuelCount { get; set; }

        public int IgnitionCount { get; set; }

        public int SnockSensor { get; set; }
    }
}
