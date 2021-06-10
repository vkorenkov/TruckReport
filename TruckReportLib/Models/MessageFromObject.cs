using TruckReportLib.Abstract;

namespace TruckReportLib.Models
{
    /// <summary>
    /// Отчет с датчиков объекта
    /// </summary>
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
        }
    }
}
