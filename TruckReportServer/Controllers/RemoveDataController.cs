using Microsoft.AspNetCore.Mvc;
using System.Net;
using TruckReportLibF.Abstract;
using TruckReportLibF.Action;
using TruckReportServer.Services;

namespace TruckReportServer.Controllers
{
    /// <summary>
    /// Контроллер удаления отчета
    /// </summary>
    [ApiController, Route("Remove")]
    public class RemoveDataController : Controller
    {
        private Reports _reports;

        public RemoveDataController(Reports reports)
        {
            _reports = reports;
        }

        /// <summary>
        /// Удаление отчета
        /// </summary>
        /// <param name="reportArray"></param>
        /// <returns></returns>
        [HttpPost, Route("UserRemoveRequest/{report}")]
        public HttpStatusCode UserRemoveRequest(byte[] reportArray)
        {
            HttpStatusCode httpStatus = default;

            if (reportArray == null)
                return HttpStatusCode.NoContent;

            Report report = ByteArray.GetObjectFromByteArray<Report>(reportArray);

            foreach(var r in _reports.reports)
            {
                if (r.id == report.id)
                {
                    _reports.reports.Remove(r);
                    httpStatus = HttpStatusCode.OK;
                    break;
                }
            }

            return httpStatus;
        }
    }
}
