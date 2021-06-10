using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using TruckReportLibF.Abstract;
using TruckReportLibF.Action;
using TruckReportLibF.Models;

namespace TruckReportClient.Request
{
    /// <summary>
    /// Класс отправляет запросы пользователя
    /// </summary>
    class UserRequests
    {
        /// <summary>
        /// Стандартная часть URL для получения данных
        /// </summary>
        const string baseUrl = "http://localhost:5200";
        /// <summary>
        /// экземпляр класса для получения данных
        /// </summary>
        private HttpClient httpClient;
        /// <summary>
        /// Результат получаемый при запросах
        /// </summary>
        private string _result;

        public UserRequests()
        {
            httpClient = new HttpClient();
        }

        /// <summary>
        /// Получение всех номеров автомобилей из хранилища данных
        /// </summary>
        /// <returns></returns>
        public async Task<List<Truck>> GetTrucks()
        {
            string url = $"{baseUrl}/Get/GetTrucks";

            try
            {
                _result = await httpClient.GetStringAsync(url);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            List<Truck> TempList = JsonConvert.DeserializeObject<List<Truck>>(_result);

            return TempList;
        }

        /// <summary>
        /// Получение отчетов для выбранного пользовотелем номера автомобиля
        /// </summary>
        /// <param name="truckNumber"></param>
        /// <returns></returns>
        public async Task<List<Report>> GetReports(string truckNumber)
        {
            List<Report> tempReportsList;

            string url = $"{baseUrl}/Get/GetReports/{truckNumber}";

            try
            {
                _result = await httpClient.GetStringAsync(url);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            byte[] tempArray = JsonConvert.DeserializeObject<byte[]>(_result);

            tempReportsList = ByteArray.GetObjectFromByteArray<List<Report>>(tempArray);

            return tempReportsList;
        }

        /// <summary>
        /// Добавление отчета по выбранному пользователем номеру автомобиля
        /// </summary>
        /// <param name="userRequest"></param>
        /// <returns></returns>
        public async Task<HttpStatusCode> AddReport(UserRequest userRequest)
        {
            string url = $"{baseUrl}/Add/UserAddRequest/{userRequest}";

            try
            {
                var result = await httpClient.PostAsync(
                    requestUri: url,
                    content: new StringContent(JsonConvert.SerializeObject(userRequest),
                    encoding: Encoding.UTF8,
                    mediaType: "application/json"));

                return result.StatusCode;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Удаление отчета по выбранному пользоваетлем номеру автомобиля
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public async Task<HttpStatusCode> RemoveReport(Report report)
        {
            byte[] reportByteArray = ByteArray.GetByteArray(report);

            string url = $"{baseUrl}/Remove/UserRemoveRequest/{reportByteArray}";

            try
            {
                var result = await httpClient.PostAsync(
                    requestUri: url,
                    content: new StringContent(JsonConvert.SerializeObject(reportByteArray),
                    encoding: Encoding.UTF8,
                    mediaType: "application/json"));

                return result.StatusCode;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
