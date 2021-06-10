using System;
using System.IO;
using TruckReportLibF.Models;

namespace TruckReportLibF.Abstract
{
    /// <summary>
    /// Абстрактный класс отчета
    /// </summary>
    [Serializable]
    public abstract class Report
    {
        public string id;

        /// <summary>
        /// Номер объекта 
        /// </summary>
        public string TruckNumber { get; set; }
        /// <summary>
        /// Должность сотрудника
        /// </summary>
        public string EmployeePosition { get; set; }
        /// <summary>
        /// Дата начала отчета
        /// </summary>
        public DateTime StartReportDate { get; set; }
        public DateTime CurrentReportDate { get; set; }
        /// <summary>
        /// Тип отчета
        /// </summary>
        public ReportType ReportType { get; set; }
        /// <summary>
        /// Переодичность формирования отчета
        /// </summary>
        public Frequency Frequency { get; set; }

        public Report(string truckNumber, string employeePosition, ReportType reportType, Frequency frequency)
        {
            id = Path.GetRandomFileName();
            TruckNumber = truckNumber;
            EmployeePosition = employeePosition;
            ReportType = reportType;
            Frequency = frequency;
        }

        public Report()
        {
        }
    }
}
