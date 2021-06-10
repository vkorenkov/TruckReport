using TruckReportLib.Models;

namespace TruckReportLib.Abstract
{
    public abstract class ReportCreator
    {
        public Truck truck;

        public string employeePosition;

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
