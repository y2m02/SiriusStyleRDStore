using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using SiriusStyleRdStore.BL.Services;
using SiriusStyleRdStore.Entities.Requests.Category;
using SiriusStyleRdStore.Entities.ViewModels;
using SiriusStyleRdStore.Entities.ViewModels.Category;
using SiriusStyleRdStore.Entities.ViewModels.EarningReport;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SiriusStyleRdStoreApp.Controllers
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