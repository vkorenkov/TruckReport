using TruckReportLib.Abstract;
using TruckReportLib.Models;

namespace TruckReportLib.Action
{
    /// <summary>
    /// Класс создания отчета движения\стоянки объекта
    /// </summary>
    public class MoveStopReportCreator : ReportCreator
    {
        public MoveStopReportCreator(Truck truck, string employeePosition, Frequency frequency) : base(truck, employeePosition, frequency)
        {
            this.truck = truck;
        }

        public override Report Create()
        {
            return new MoveStop(truck.TruckNumber, employeePosition, truck.MoveTime, truck.StopTime, ReportType.MoveStop, frequency);
        }
    }
}
