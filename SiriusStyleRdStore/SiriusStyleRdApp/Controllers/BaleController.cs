using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using SiriusStyleRd.Entities.Requests.Bale;
using SiriusStyleRd.Entities.ViewModels;
using SiriusStyleRd.Entities.ViewModels.Bale;
using SiriusStyleRd.Services.Services;

namespace SiriusStyleRdApp.Controllers
{
    public class BaleController : Controller
    {
        private readonly IBaleService _baleService;
        private readonly IMapper _mapper;

        public BaleController(IBaleService baleService, IMapper mapper)
        {
            _baleService = baleService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetAll([DataSourceRequest] DataSourceRequest request)
        {
            var response = await _baleService.GetAll().ConfigureAwait(false);

            if (response is Success<IEnumerable<BaleViewModel>> bales)
                return Json(await bales.Response.ToDataSourceResultAsync(request));

            throw new Exception();
        }

        [HttpPost]
        public async Task<IActionResult> BatchCreate([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<BaleViewModel> bales)
        {
            var results = new List<BaleViewModel>();

            if (ModelState.IsValid)
            {
                var response =
                    await _baleService.BatchCreate(_mapper.Map<List<CreateBaleRequest>>(bales.ToList()));

                if (response is Success<BaleViewModel> result)
                {
                    results.Add(result.Response);
                }
            }

            return Json(await results.ToDataSourceResultAsync(request, ModelState));
        }

        [HttpPost]
        public async Task<IActionResult> BatchUpdate([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<BaleViewModel> bales)
        {
            var baleList = bales.ToList();
            if (ModelState.IsValid)
            {
                var _ = await _baleService.BatchUpdate(
                    _mapper.Map<List<UpdateBaleRequest>>(baleList.ToList()));
            }

            return Json(await baleList.ToDataSourceResultAsync(request, ModelState));
        }

        [HttpPost]
        public async Task<IActionResult> BatchDelete([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<BaleViewModel> bales)
        {
            var baleList = bales.ToList();

            if (ModelState.IsValid)
            {
                var _ = await _baleService.BatchDelete(
                    _mapper.Map<List<DeleteBaleRequest>>(baleList.ToList()));
            }

            return Json(await baleList.ToDataSourceResultAsync(request, ModelState));
        }

        public async Task<JsonResult> GetAllForDropDownList(int? id)
        {
            var response = await _baleService.GetAllForDropDownList(id.GetValueOrDefault()).ConfigureAwait(false);

            if (response is Success<IEnumerable<BaleViewModel>> bales)
                return Json(bales.Response
                    .Select(c => new
                    {
                        id = c.BaleId,
                        description = c.Description
                    }));

            throw new Exception();
        }
    }
}