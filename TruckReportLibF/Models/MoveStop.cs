using System;
using TruckReportLibF.Abstract;
using TruckReportLibF.Action;

namespace TruckReportLibF.Models
{

    /// <summary>
    /// Отчет движения\стоянки объекта
    /// </summary>
    [Serializable]
    public class MoveStop : Report
    {
        /// <summary>
        /// Время проведенное в движении
        /// </summary>
        public float MoveTime { get; set; }
        /// <summary>
        /// Время проведенное без движения 
        /// </summary>
        public float StopTime { get; set; }

        public MoveStop(string truckNumber, string employeePosition, float moveTime, float stopTime, ReportType reportType, Frequency frequency) :
            base(truckNumber, employeePosition, reportType, frequency)
        {
            MoveTime = moveTime;
            StopTime = stopTime;
            StartReportDate = DateTime.Now;
            CurrentReportDate = StartReportDate;
        }
    }
}
