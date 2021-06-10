using System;
using TruckReportLib.Models;

namespace TruckReportLib.Abstract
{
    public abstract class Report
    {
        /// <summary>
        /// Номер объекта 
        /// </summary>
        public string TruckNumber { get; set; }
        /// <summary>
        /// Должность сотрудника
        /// </summary>
        public string EmployeePosition { get; set; }

        public DateTime StartReportDate { get; set; }

        public ReportType ReportType { get; set; }

        public Frequency Frequency { get; set; }

        public Report(string truckNumber, string employeePosition, ReportType reportType, Frequency frequency)
        {
            TruckNumber = truckNumber;
            EmployeePosition = employeePosition;
            ReportType = reportType;
            Frequency = frequency;
        }
    }
}
