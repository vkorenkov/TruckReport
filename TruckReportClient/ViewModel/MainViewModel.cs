using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TruckReportClient.Helpers;
using TruckReportClient.Request;
using TruckReportLibF.Abstract;
using TruckReportLibF.Models;

namespace TruckReportClient.ViewModel
{
    class MainViewModel : PropertyChange
    {
        private string outputMessage;
        /// <summary>
        /// Вывод технической информации
        /// </summary>
        public string OutputMessage
        {
            get => outputMessage;
            set { outputMessage = value; OnPropertyChanged(nameof(OutputMessage)); }
        }

        /// <summary>
        /// Список типов отчетов
        /// </summary>
        public List<ReportType> ReportTypeList { get; set; }
        /// <summary>
        /// Список типов переодичности
        /// </summary>
        public List<Frequency> FrequencyList { get; set; }

        private ObservableCollection<Truck> trucks;
        /// <summary>
        /// Список всех номеров автомобилей
        /// </summary>
        public ObservableCollection<Truck> Trucks
        {
            get => trucks;
            set { trucks = value; OnPropertyChanged(nameof(Trucks)); }
        }

        private Report selectedReport;
        /// <summary>
        /// Выбранный отчет
        /// </summary>
        public Report SelectedReport
        {
            get => selectedReport;
            set { selectedReport = value; OnPropertyChanged(nameof(SelectedReport)); }
        }

        private string employeePosition;
        /// <summary>
        /// Должность введенная пользователем
        /// </summary>
        public string EmployeePosition
        {
            get => employeePosition;
            set { employeePosition = value; OnPropertyChanged(nameof(EmployeePosition)); }
        }

        private ReportType selectedReportType;
        /// <summary>
        /// Выбранный пользователем тип отчета
        /// </summary>
        public ReportType SelectedReportType
        {
            get => selectedReportType;
            set { selectedReportType = value; OnPropertyChanged(nameof(SelectedReportType)); }
        }

        private Frequency selectedFrequency;
        /// <summary>
        /// Выбранная пользователем переодичность
        /// </summary>
        public Frequency SelectedFrequency
        {
            get => selectedFrequency;
            set { selectedFrequency = value; OnPropertyChanged(nameof(SelectedFrequency)); }
        }

        private Truck selectedTruck;
        /// <summary>
        /// Выбранный пользоателем номер автомобиля
        /// </summary>
        public Truck SelectedTruck
        {
            get => selectedTruck;
            set
            {
                selectedTruck = value; OnPropertyChanged(nameof(SelectedTruck)); GetReports();
            }
        }

        /// <summary>
        /// Объект запросов пользователя
        /// </summary>
        private UserRequests _userRequests;

        /// <summary>
        /// Полученные статусы запросов
        /// </summary>
        private HttpStatusCode httpResponse;

        /// <summary>
        /// Команда обновления данных
        /// </summary>
        public ICommand RefreshData => new RelayCommand<object>(async obj =>
        {
            if (SelectedTruck == null)
            {
                MessageBox.Show("Выберите один из номеров автомобилей");
                return;
            }

            await GetReports();
            OutputMessage = $"Отчеты для {SelectedTruck.TruckNumber} обновлены. Время обновления {DateTime.Now.ToShortTimeString()}";
        });

        /// <summary>
        /// Команда добавления нового отчета
        /// </summary>
        public ICommand AddReport => new RelayCommand<object>(async obj =>
        {
            if (SelectedTruck == null)
            {
                MessageBox.Show("Выберите один из номеров автомобилей");
                return;
            }
            if (string.IsNullOrWhiteSpace(EmployeePosition))
            {
                MessageBox.Show("Введите должность");
                return;
            }

            httpResponse = await _userRequests.AddReport(new UserRequest(SelectedTruck.TruckNumber, EmployeePosition, SelectedReportType, SelectedFrequency));

            if (httpResponse == HttpStatusCode.OK)
            {
                await GetReports();
                OutputMessage = $"Отчет для автомобиля {SelectedTruck.TruckNumber} добавлен. Время добавления {DateTime.Now.ToShortTimeString()}";
            }
            else
            {
                OutputMessage = $"Ошибка добавления отчета. Код ошибки: {httpResponse}";
            }
        });

        /// <summary>
        /// Команда удаления выбранного пользователем отчета
        /// </summary>
        public ICommand RemoveReport => new RelayCommand<object>(async obj =>
        {
            if (SelectedReport != null)
            {
                httpResponse = await _userRequests.RemoveReport(SelectedReport);

                if (httpResponse == HttpStatusCode.OK)
                {
                    await GetReports();
                    OutputMessage = $"Отчет для автомобиля {SelectedTruck.TruckNumber} удален. Время удаления {DateTime.Now.ToShortTimeString()}";
                }
                else
                {
                    OutputMessage = $"Ошибка удаления отчета. Код ошибки: {httpResponse}";
                }
            }
            else
                OutputMessage = "Выберите один из отчетов";
        });

        public MainViewModel()
        {
            _userRequests = new UserRequests();

            ReportTypeList = Enum.GetValues(typeof(ReportType)).OfType<ReportType>().ToList();
            FrequencyList = Enum.GetValues(typeof(Frequency)).OfType<Frequency>().ToList();

            Task.Run(() =>
            {
                try
                {
                    GetTrucks().Wait();
                }
                catch (Exception e)
                {
                    OutputMessage = $"Ошибка получения объектов автомобилей. Код ошибки: {e.InnerException.Message}";
                }
            });
        }

        /// <summary>
        /// Получение всех номеров автомобилей из хранилища данных
        /// </summary>
        /// <returns></returns>
        private async Task GetTrucks()
        {
            Trucks = new ObservableCollection<Truck>(await _userRequests.GetTrucks());
        }

        /// <summary>
        /// Получение всех отчетов для выбранного пользователем номера автомобиля
        /// </summary>
        /// <returns></returns>
        private async Task GetReports()
        {
            if (SelectedTruck != null)
            {
                var tempList = new ObservableCollection<Report>(await _userRequests.GetReports(SelectedTruck.TruckNumber));

                SelectedTruck.MessageFromObjectReports.Clear();
                SelectedTruck.MoveStopReports.Clear();

                foreach (var t in tempList)
                {
                    if (t.ReportType == ReportType.MoveStop)
                        SelectedTruck.MoveStopReports.Add((MoveStop)t);
                    if (t.ReportType == ReportType.MessageFromObject)
                        SelectedTruck.MessageFromObjectReports.Add((MessageFromObject)t);
                }
            }
        }
    }
}
