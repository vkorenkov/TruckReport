using System;
using TruckReportLib.Abstract;

namespace TruckReportLib.Models
{
    /// <summary>
    /// Отчет движения\стоянки объекта
    /// </summary>
    public class MoveStop : Report
    {
        /// <summary>
        /// Время проведенное в движении
        /// </summary>
        public DateTime? MoveTime { get; set; }
        /// <summary>
        /// Время проведенное без движения 
        /// </summary>
        public DateTime? StopTime { get; set; }

        public MoveStop(string truckNumber, string employeePosition, DateTime? moveTime, DateTime? stopTime, ReportType reportType, Frequency frequency) :
            base(truckNumber, employeePosition, reportType, frequency)
        {
            MoveTime = moveTime;
            StopTime = stopTime;
        }
    }
}
