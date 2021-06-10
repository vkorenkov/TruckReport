namespace TruckReportLibF.Models
{
    /// <summary>
    /// Перечисление переодичности формирования отчета
    /// </summary>
    public enum Frequency
    {
        day,
        week,
        month
    }
    /// <summary>
    /// Перечисление типов отчетов
    /// </summary>
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
        /// <summary>
        /// Тип отчета
        /// </summary>
        public ReportType ReportType { get; set; }
        /// <summary>
        /// Переодичность отчета
        /// </summary>
        public Frequency Frequency { get; set; }

        public UserRequest(string truckNumber, string employeePosition, ReportType reportType, Frequency frequency)
        {
            TruckNumber = truckNumber;
            EmployeePosition = employeePosition;
            ReportType = reportType;
            Frequency = frequency;
        }

        public UserRequest()
        {
        }
    }
}
