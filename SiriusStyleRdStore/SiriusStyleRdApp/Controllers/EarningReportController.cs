using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using SiriusStyleRd.Entities.ViewModels;
using SiriusStyleRd.Entities.ViewModels.EarningReport;
using SiriusStyleRd.Services.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SiriusStyleRdApp.Controllers
{
    public class EarningReportController : Controller
    {
        private readonly IEarningReportService _earningReportService;

        public EarningReportController(IEarningReportService earningReportService)
        {
            _earningReportService = earningReportService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> Get([DataSourceRequest] DataSourceRequest request)
        {
            var response = await _earningReportService.GetReport().ConfigureAwait(false);

            if (response is Success<List<EarningReportViewModel>> reports)
                return Json(await reports.Response.ToDataSourceResultAsync(request));

            throw new Exception();
        }
    }
}