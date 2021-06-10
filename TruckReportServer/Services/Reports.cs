using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using TruckReportLibF.Abstract;
using TruckReportLibF.Action;
using TruckReportLibF.Models;

namespace TruckReportServer.Services
{
    // Для тестирования измените значение логического поля isTest на true в строке 30.
    // В случае isTest = true, тестовый день будет равен 5 секундам, неделя равна 7 секундам, а месяц равен 30 секундам.
    // Таймер срабатывает каждые условные сутки, если isTest = true, и каждые реальные сутки если isTest = false.
    // Если вы хотите установить свои параметры, то измените интервал срабатывания таймера (имитация суток) в строке 61,
    // первый параметр в вызове метода Checkdate, строка 177 (имитация недели) и переменную dayInMonth в строке 188 (имитация месяца).
    // Так же есть первый вариант с несклькими таймерами. Описание в #region IfTimersMoreThenOne в строке 55.

    /// <summary>
    /// Класс создание тестовых отчетов
    /// </summary>
    public class Reports
    {
        /// <summary>
        /// Список всех существующих отчетов
        /// </summary>
        public List<Report> reports;
        /// <summary>
        /// Список всех автомобилей
        /// </summary>
        private TruckCreator _truckCreator;
        /// <summary>
        /// поле для тестирования
        /// </summary>
        private bool isTest = false;

        #region Three timers
        //Timer dayTimer;
        //Timer weekTimer;
        //Timer monthTimer;
        #endregion
        /// <summary>
        /// Таймер проверки отчетов
        /// </summary>
        private Timer _timer;
        /// <summary>
        /// Интервал срабатывания таймера
        /// </summary>
        private float dayInterval;

        public Reports(TruckCreator truckCreator)
        {
            reports = new List<Report>();

            _truckCreator = truckCreator;

            FakeReport();

            #region "IfTimersMoreThenOne" Test variant with milliseconds. Don't forget uncomment fields region called "Three timers" and "More Timers" in Timer_Elapsed method and comment One timer
            //dayTimer = SetTimers(1000, Timer_Elapsed);
            //weekTimer = SetTimers(2000, Timer_Elapsed);
            //monthTimer = SetTimers(3000, Timer_Elapsed);
            #endregion

            if (!isTest)
                dayInterval = 60 * 60 * 1000 * 24;
            else
                dayInterval = 5000;

            _timer = SetTimers(dayInterval, Timer_Elapsed);
        }

        /// <summary>
        /// Создание тестовых отчетов
        /// </summary>
        private void FakeReport()
        {
            Random r = new Random();

            Array reportTypeValues = Enum.GetValues(typeof(ReportType));
            Array frequencyValues = Enum.GetValues(typeof(Frequency));

            for (int i = 0; i < 5; i++)
            {
                var truck = new Truck()
                {
                    TruckNumber = TruckCreator.truckNumbers[r.Next(TruckCreator.truckNumbers.Length)],
                    CurrentFuelCount = r.Next(10, 101),
                    IgnitionCount = r.Next(10, 1000),
                    MoveTime = r.Next(0, 101),
                    StopTime = r.Next(0, 101),
                    SnockSensor = r.Next(100, 10000)
                };

                var tempReportType = (ReportType)reportTypeValues.GetValue(r.Next(reportTypeValues.Length));

                Report report = null;

                switch (tempReportType)
                {
                    case ReportType.MessageFromObject:
                        report = new MessageFromObjectReportCreator(truck, "test", (Frequency)frequencyValues.GetValue(r.Next(frequencyValues.Length))).Create();
                        break;
                    case ReportType.MoveStop:
                        report = new MoveStopReportCreator(truck, "test", (Frequency)frequencyValues.GetValue(r.Next(frequencyValues.Length))).Create();
                        break;
                }

                reports.Add(report);
            }
        }

        /// <summary>
        /// Создание отчетов
        /// </summary>
        /// <param name="truck"></param>
        /// <param name="emploeePosition"></param>
        /// <param name="frequency"></param>
        /// <param name="reportType"></param>
        /// <returns></returns>
        public Report ReportCreator(Truck truck, string emploeePosition, Frequency frequency, ReportType reportType)
        {
            ReportCreator report;
            Report truckReport = null;

            switch (reportType)
            {
                case ReportType.MessageFromObject:
                    report = new MessageFromObjectReportCreator(truck, emploeePosition, frequency);
                    truckReport = report.Create();
                    break;
                case ReportType.MoveStop:
                    report = new MoveStopReportCreator(truck, emploeePosition, frequency);
                    truckReport = report.Create();
                    break;
            }

            truckReport.CurrentReportDate = DateTime.Now;

            return truckReport;
        }

        /// <summary>
        /// Изменение отчетов
        /// </summary>
        /// <param name="truck"></param>
        /// <param name="report"></param>
        /// <returns></returns>
        private Report ReportChanger(Truck truck, Report report)
        {
            return ReportCreator(truck, report.EmployeePosition, report.Frequency, report.ReportType);
        }

        /// <summary>
        /// Установка интервала таймеров
        /// </summary>
        /// <param name="time"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        private Timer SetTimers(float time, ElapsedEventHandler handler)
        {
            var timer = new Timer(time);
            timer.Elapsed += handler;
            timer.AutoReset = true;
            timer.Enabled = true;

            return timer;
        }

        /// <summary>
        /// Обработчик события таймера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            #region One timer
            for (int i = 0; i < reports.Count; i++)
            {
                if (reports[i].Frequency == Frequency.day)
                {
                    reports[i] = ReportListHendler(reports[i]);
                }

                if (reports[i].Frequency == Frequency.week)
                {
                    if (Checkdate(7, reports[i].CurrentReportDate))
                    {
                        reports[i] = ReportListHendler(reports[i]);
                    }
                }

                if (reports[i].Frequency == Frequency.month)
                {
                    int dayInMonth = default;

                    if (!isTest)
                        dayInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month);
                    else
                        dayInMonth = 30;

                    if (Checkdate(dayInMonth, reports[i].CurrentReportDate))
                    {
                        reports[i] = ReportListHendler(reports[i]);
                    }
                }
            }
            #endregion

            #region More Timers
            //List<Report> tempReportList = null;

            //Timer timer = sender as Timer;

            //if (timer.Interval == dayTimer.Interval)
            //    tempReportList = reports.Where(x => x.Frequency == Frequency.day).ToList();
            //if (timer.Interval == weekTimer.Interval)
            //    tempReportList = reports.Where(x => x.Frequency == Frequency.week).ToList();
            //if (timer.Interval == monthTimer.Interval)
            //    tempReportList = reports.Where(x => x.Frequency == Frequency.month).ToList();

            //ReportListHendler(tempReportList);
            #endregion
        }

        /// <summary>
        /// Проверка даты формирования отчета
        /// </summary>
        /// <param name="dayCount"></param>
        /// <param name="reportDate"></param>
        /// <returns></returns>
        private bool Checkdate(double dayCount, DateTime reportDate)
        {
            #region Original
            //DateTime date = DateTime.Now - TimeSpan.FromDays(dayCount);

            //if (reportDate.Day == date.Day /*reportDate.Second == date.Second*/)
            //{
            //    Console.WriteLine(dayCount);
            //    return true;
            //}
            //else
            //    return false;
            #endregion

            #region for test
            int date = default;
            int equaleReportDate = default;

            if (!isTest)
            {
                equaleReportDate = reportDate.Day;
                date = (DateTime.Now - TimeSpan.FromDays(dayCount)).Day;
            }
            else
            {
                equaleReportDate = reportDate.Second;
                date = (DateTime.Now - TimeSpan.FromDays(dayCount)).Second;
            }

            if (equaleReportDate == date)
                return true;
            else
                return false;
            #endregion
        }

        /// <summary>
        /// Обработка данных на основе периодичности
        /// </summary>
        /// <param name="temp"></param>
        private void ReportListHendler(List<Report> receivedReports)
        {
            if (receivedReports == null)
                return;

            for (int i = 0; i < receivedReports.Count; i++)
            {
                Truck truck = _truckCreator.trucks.Where(x => x.TruckNumber == receivedReports[i].TruckNumber).FirstOrDefault();

                if (receivedReports[i].TruckNumber == truck.TruckNumber)
                    reports[reports.IndexOf(receivedReports[i])] = ReportChanger(truck, receivedReports[i]);
            }
        }

        /// <summary>
        /// Обработка данных на основе периодичности
        /// </summary>
        /// <param name="temp"></param>
        private Report ReportListHendler(Report receivedReport)
        {
            Truck truck = _truckCreator.trucks.Where(x => x.TruckNumber == receivedReport.TruckNumber).FirstOrDefault();

            receivedReport = ReportChanger(truck, receivedReport);

            return receivedReport;
        }
    }
}
