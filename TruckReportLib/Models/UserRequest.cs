using System;
using TruckReportLib.Abstract;

namespace TruckReportLib.Models
{
    public enum Frequency
    {
        day,
        week,
        month
    }

    public enum ReportType
    {
        MoveStop,
        MessageFromObject
    }

    /// <summary>
    /// Отчет по объекту
    /// </summary>
    public class UserRequest
    {
        /// <summary>
        /// Номер объекта 
        /// </summary>
        public string TruckNumber { get; set; }
        /// <summary>
        /// Должность сотрудника
        /// </summary>
        public string EmployeePosition { get; set; }

        public ReportType ReportType { get; set; }
       
        public Frequency Frequency { get; set; }

        public UserRequest(string truckNumber, string employeePosition, ReportType reportType, Frequency frequency)
        {
            TruckNumber = truckNumber;
            EmployeePosition = employeePosition;
            ReportType = reportType;
            Frequency = frequency;
        }
    }
}
