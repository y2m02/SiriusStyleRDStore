using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using SiriusStyleRdStore.BL.Services;
using SiriusStyleRdStore.Entities.Requests.Product;
using SiriusStyleRdStore.Entities.ViewModels;
using SiriusStyleRdStore.Entities.ViewModels.Category;
using SiriusStyleRdStore.Entities.ViewModels.Product;
using SiriusStyleRdStore.Entities.ViewModels.Size;
using SiriusStyleRdStore.Utility.Extensions;

namespace SiriusStyleRdStoreApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ISizeService _sizeService;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ProductController(IProductService productService, 
            IMapper mapper, 
            ICategoryService categoryService, 
            ISizeService sizeService)
        {
            _productService = productService;
            _mapper = mapper;
            _categoryService = categoryService;
            _sizeService = sizeService;
        }

        public async Task<IActionResult> Index()
        {
            if (await _categoryService.GetAll() is Success<IEnumerable<CategoryViewModel>> categories)
            {
                ViewBag.Categories = categories.Response;
            }

            if (await _sizeService.GetAll() is Success<IEnumerable<SizeViewModel>> sizes)
            {
                ViewBag.Sizes = sizes.Response;
            }

            return View();
        }

        public async Task<JsonResult> GetAll([DataSourceRequest] DataSourceRequest request)
        {
            var response = await _productService.GetAll().ConfigureAwait(false);

            if (response is Success<IEnumerable<ProductViewModel>> products)
                return Json(await products.Response.ToDataSourceResultAsync(request));

            throw new Exception();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ProductRequest product)
        {
            if (product.ProductCode.HasValue())
            {
                var update = _mapper.Map<UpdateProductRequest>(product);
                update.UpdateImage = product.Image.HasValue();

                var _ = await _productService.Update(update);

                return RedirectToAction(nameof(Index));
            }

            _ = await _productService.Create(_mapper.Map<CreateProductRequest>(product));

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<ActionResult> BatchCreate([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<ProductViewModel> products)
        {
            var results = new List<ProductViewModel>();

            if (products != null && ModelState.IsValid)
            {
                var response =
                    await _productService.BatchCreate(_mapper.Map<List<CreateProductRequest>>(products.ToList()));

                if (response is Success<ProductViewModel> result) results.Add(result.Response);
            }

            return Json(await results.ToDataSourceResultAsync(request, ModelState));
        }

        [HttpPost]
        public async Task<ActionResult> BatchUpdate([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<ProductViewModel> products)
        {
            var productList = products.ToList();
            if (ModelState.IsValid)
            {
                var _ = await _productService.BatchUpdate(
                    _mapper.Map<List<UpdateProductRequest>>(productList.ToList()));
            }

            return Json(await productList.ToDataSourceResultAsync(request, ModelState));
        }
    }
}