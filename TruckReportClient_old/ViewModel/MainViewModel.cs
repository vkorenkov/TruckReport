using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TruckReportLibF.Abstract;
using TruckReportLibF.Models;
using Newtonsoft.Json;
using TruckReportClient.Request;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TruckReportClient.ViewModel
{
    class MainViewModel
    {
        public List<string> TruckNumbers { get; set; }

        private UserRequests _userRequests;

        public MainViewModel()
        {
            TruckNumbers = new List<string>();

            _userRequests = new UserRequests();

            GetTruckNumbers();
        }

        private async Task GetTruckNumbers()
        {
            TruckNumbers = await _userRequests.GetTrucksNumbers();
        }
    }
}
