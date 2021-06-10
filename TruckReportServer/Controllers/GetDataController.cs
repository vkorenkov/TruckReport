using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using TruckReportLibF.Abstract;
using TruckReportLibF.Action;
using TruckReportLibF.Models;
using TruckReportServer.Services;

namespace TruckReportServer.Controllers
{
    [ApiController, Route("Get")]
    public class GetDataController : Controller
    {
        private TruckCreator _truckCreator;
        private Reports _reports;

        public GetDataController(TruckCreator truckCreator, Reports reports)
        {
            _truckCreator = truckCreator;

            _reports = reports;
        }

        [HttpGet, Route("GetTrucks")]
        public List<Truck> GetTrucks()
        {
            return _truckCreator.trucks;
        }

        [HttpGet, Route("GetReports/{truckNumber}")]
        public byte[] GetReports(string truckNumber)
        {
            BinaryFormatter bf = new BinaryFormatter();

            List<Report> tempReportsList = _reports.reports.Where(r => r.TruckNumber == truckNumber).ToList();

            byte[] reportsByteArray = ByteArray.GetByteArray(tempReportsList);

            return reportsByteArray;            
        }
    }
}
