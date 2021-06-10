using TruckReportLib.Abstract;
using TruckReportLib.Models;

namespace TruckReportLib.Action
{
    /// <summary>
    /// Класс создания отчета датчиков объекта
    /// </summary>
    public class MessageFromObjectReportCreator : ReportCreator
    {
        public MessageFromObjectReportCreator(Truck truck, string employeePosition, Frequency frequency) : base(truck, employeePosition, frequency)
        {
            this.truck = truck;
        }

        public override Report Create()
        {
            return new MessageFromObject(truck.TruckNumber, employeePosition, truck.CurrentFuelCount, truck.IgnitionCount, truck.SnockSensor, ReportType.MessageFromObject, frequency);
        }
    }
}
