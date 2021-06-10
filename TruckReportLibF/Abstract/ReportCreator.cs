using TruckReportLibF.Models;

namespace TruckReportLibF.Abstract
{
    /// <summary>
    /// Абстрактный класс создания отчетов
    /// </summary>
    public abstract class ReportCreator
    {
        /// <summary>
        /// Объект автомобиля
        /// </summary>
        public Truck truck;
        /// <summary>
        /// Должность ответственного
        /// </summary>
        public string employeePosition;
        /// <summary>
        /// Переодичность отчетов
        /// </summary>
        public Frequency frequency;

        public ReportCreator(Truck truck, string employeePosition, Frequency frequency)
        {
            this.truck = truck;
            this.employeePosition = employeePosition;
            this.frequency = frequency;
        }

        /// <summary>
        /// Возвращает отчет объекта
        /// </summary>
        /// <returns></returns>
        abstract public Report Create();
    }
}
