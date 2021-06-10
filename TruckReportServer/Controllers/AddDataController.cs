using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TruckReportLibF.Abstract;
using TruckReportLibF.Models;
using TruckReportServer.Services;

namespace TruckReportServer.Controllers
{
    /// <summary>
    /// Контроллер добавления отчета
    /// </summary>
    [ApiController, Route("Add")]
    public class AddDataController : Controller
    {
        private TruckCreator _truckCreator;
        private Reports _reports;

        public AddDataController(TruckCreator truckCreator, Reports reports)
        {
            _truckCreator = truckCreator;

            _reports = reports;
        }

        /// <summary>
        /// Добавление нового отчета
        /// </summary>
        /// <param name="userRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UserAddRequest/{userRequest}")]
        public HttpStatusCode UserAddRequest(UserRequest userRequest)
        {
            if (userRequest == null)
                return HttpStatusCode.NoContent;

            Report report = null;

            Truck truck = _truckCreator.trucks.Where(x => x.TruckNumber == userRequest.TruckNumber).FirstOrDefault();

            if (truck != null)
            {
                report = _reports.ReportCreator(truck, userRequest.EmployeePosition, userRequest.Frequency, userRequest.ReportType);

                _reports.reports.Add(report);

                return HttpStatusCode.OK;
            }
            else
                return HttpStatusCode.NoContent;
        }
    }
}
