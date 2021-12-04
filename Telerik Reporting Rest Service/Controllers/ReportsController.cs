using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Telerik_Reporting_Rest_Service.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Telerik.Reporting.Services;
    using Telerik.Reporting.Services.AspNetCore;

    [Route("api/reports")]
    public class ReportsController : ReportsControllerBase
    {
        public ReportsController(IReportServiceConfiguration reportServiceConfiguration)
            : base(reportServiceConfiguration)
        {
        }
    }
}
