﻿using System;
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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SiriusStyleRdStoreApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetAll([DataSourceRequest] DataSourceRequest request)
        {
            var response = await _categoryService.GetAll().ConfigureAwait(false);

            if (response is Success<IEnumerable<CategoryViewModel>> Categorys)
                return Json(await Categorys.Response.ToDataSourceResultAsync(request));

            throw new Exception();
        }

        [HttpPost]
        public async Task<ActionResult> BatchCreate([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<CategoryViewModel> categories)
        {
            var results = new List<CategoryViewModel>();

            if (ModelState.IsValid)
            {
                var response = await _categoryService.BatchCreate(_mapper.Map<List<CreateCategoryRequest>>(categories.ToList()));

                if (response is Success<CategoryViewModel> result)
                {
                    results.Add(result.Response);
                }
            }

            return Json(await results.ToDataSourceResultAsync(request, ModelState));
        }

        [HttpPost]
        public async Task<ActionResult> BatchUpdate([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<CategoryViewModel> categories)
        {
            var categoryList = categories.ToList();
            if (ModelState.IsValid)
            {
                var _ = await _categoryService.BatchUpdate(_mapper.Map<List<UpdateCategoryRequest>>(categoryList.ToList()));
            }

            return Json(await categoryList.ToDataSourceResultAsync(request, ModelState));
        }

        [HttpPost]
        public async Task<ActionResult> BatchDelete([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<CategoryViewModel> categories)
        {
            var categoryList = categories.ToList();

            if (ModelState.IsValid)
            {
                var _ = await _categoryService.BatchDelete(_mapper.Map<List<DeleteCategoryRequest>>(categoryList.ToList()));
            }

            return Json(await categoryList.ToDataSourceResultAsync(request, ModelState));
        }
    }
}