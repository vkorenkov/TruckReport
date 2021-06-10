using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using TruckReportLibF.Abstract;
using TruckReportLibF.Models;

namespace TruckReportClient.Request
{
    class UserRequests
    {
        const string baseUrl = "http://localhost:5200/TruckReportApi";

        private HttpClient httpClient;

        public UserRequests()
        {
            httpClient = new HttpClient();
        }

        public async Task<List<string>> GetTrucksNumbers()
        {
            string url = $"{baseUrl}/GetData/GetTrucks";

            return JsonConvert.DeserializeObject<List<string>>(await httpClient.GetStringAsync(url));
        }

        public List<Report> GetReports()
        {
            List<Report> tempReportsList;

            string url = $"{baseUrl}/GetData/GetReports";

            byte[] tempArray;

            tempArray = Task.Run(async () => JsonConvert.DeserializeObject<byte[]>(await httpClient.GetStringAsync(url))).Result;

            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                ms.Write(tempArray, 0, tempArray.Length);
                ms.Seek(0, SeekOrigin.Begin);
                tempReportsList = (List<Report>)bf.Deserialize(ms);
            }

            return tempReportsList;
        }
    }
}
