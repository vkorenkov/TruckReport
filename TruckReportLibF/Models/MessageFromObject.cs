using System;
using TruckReportLibF.Abstract;
using TruckReportLibF.Action;

namespace TruckReportLibF.Models
{
    /// <summary>
    /// Отчет с датчиков объекта
    /// </summary>
    [Serializable]
    public class MessageFromObject : Report
    {
        /// <summary>
        /// Текущее количество топлива
        /// </summary>
        public float CurrentFuelCount { get; set; }
        /// <summary>
        /// Количество сенсора зажигания
        /// </summary>
        public int IgnitionCount { get; set; }
        /// <summary>
        /// Количество срабатываний сенсора детонации
        /// </summary>
        public int SnockSensor { get; set; }

        public MessageFromObject(string truckNumber, string employeePosition, float currentFuelCount, int ignitionCount, int snockSensor, ReportType reportType, Frequency frequency) : 
            base(truckNumber, employeePosition, reportType, frequency)
        {
            CurrentFuelCount = currentFuelCount;
            IgnitionCount = ignitionCount;
            SnockSensor = snockSensor;
            StartReportDate = DateTime.Now;
            CurrentReportDate = StartReportDate;
        }
    }
}
