using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using SiriusStyleRd.Entities.Requests.Size;
using SiriusStyleRd.Entities.ViewModels;
using SiriusStyleRd.Entities.ViewModels.Size;
using SiriusStyleRd.Services.Services;

namespace SiriusStyleRdApp.Controllers
{
    public class SizeController : Controller
    {
        private readonly ISizeService _sizeService;
        private readonly IMapper _mapper;

        public SizeController(ISizeService sizeService, IMapper mapper)
        {
            _sizeService = sizeService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetAll([DataSourceRequest] DataSourceRequest request)
        {
            var response = await _sizeService.GetAll().ConfigureAwait(false);

            if (response is Success<IEnumerable<SizeViewModel>> sizes)
                return Json(await sizes.Response.ToDataSourceResultAsync(request));

            throw new Exception();
        }

        [HttpPost]
        public async Task<IActionResult> BatchCreate([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<SizeViewModel> sizes)
        {
            var results = new List<SizeViewModel>();

            if (ModelState.IsValid)
            {
                var response =
                    await _sizeService.BatchCreate(_mapper.Map<List<CreateSizeRequest>>(sizes.ToList()));

                if (response is Success<SizeViewModel> result)
                {
                    results.Add(result.Response);
                }
            }

            return Json(await results.ToDataSourceResultAsync(request, ModelState));
        }

        [HttpPost]
        public async Task<IActionResult> BatchUpdate([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<SizeViewModel> sizes)
        {
            var sizeList = sizes.ToList();
            if (ModelState.IsValid)
            {
                var _ = await _sizeService.BatchUpdate(
                    _mapper.Map<List<UpdateSizeRequest>>(sizeList.ToList()));
            }

            return Json(await sizeList.ToDataSourceResultAsync(request, ModelState));
        }

        [HttpPost]
        public async Task<IActionResult> BatchDelete([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<SizeViewModel> sizes)
        {
            var sizeList = sizes.ToList();

            if (ModelState.IsValid)
            {
                var _ = await _sizeService.BatchDelete(
                    _mapper.Map<List<DeleteSizeRequest>>(sizeList.ToList()));
            }

            return Json(await sizeList.ToDataSourceResultAsync(request, ModelState));
        }

        public async Task<JsonResult> GetAllForDropDownList(int? id)
        {
            var response = await _sizeService.GetAllForDropDownList(id.GetValueOrDefault()).ConfigureAwait(false);

            if (response is Success<IEnumerable<SizeViewModel>> sizes)
                return Json(sizes.Response
                    .Select(w => new
                    {
                        id = w.SizeId,
                        description = w.Description
                    }));

            throw new Exception();
        }
    }
}
