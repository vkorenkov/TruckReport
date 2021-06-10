using System.Collections.ObjectModel;

namespace TruckReportLibF.Models
{
    /// <summary>
    /// Модель данных автомобиля
    /// </summary>
    public class Truck
    {
        /// <summary>
        /// Номер автомобиля
        /// </summary>
        public string TruckNumber { get; set; }
        /// <summary>
        /// Общее время в движении
        /// </summary>
        public float MoveTime { get; set; }
        /// <summary>
        /// Общее время стоянки
        /// </summary>
        public float StopTime { get; set; }
        /// <summary>
        /// Текущее количество топлива
        /// </summary>
        public float CurrentFuelCount { get; set; }
        /// <summary>
        /// Количество запусков зажигания
        /// </summary>
        public int IgnitionCount { get; set; }
        /// <summary>
        /// Количество страбатываний детонации
        /// </summary>
        public int SnockSensor { get; set; }
        /// <summary>
        /// Список отчетов
        /// </summary>
        public ObservableCollection<MoveStop> MoveStopReports { get; set; }

        public ObservableCollection<MessageFromObject> MessageFromObjectReports { get; set; }

        public Truck()
        {
            MessageFromObjectReports = new ObservableCollection<MessageFromObject>();
            MoveStopReports = new ObservableCollection<MoveStop>();
        }
    }
}
