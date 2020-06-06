using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using SiriusStyleRdStore.BL.Services;
using SiriusStyleRdStore.Entities.Enums;
using SiriusStyleRdStore.Entities.Requests.Product;
using SiriusStyleRdStore.Entities.ViewModels;
using SiriusStyleRdStore.Entities.ViewModels.Category;
using SiriusStyleRdStore.Entities.ViewModels.Item;
using SiriusStyleRdStore.Entities.ViewModels.Product;
using SiriusStyleRdStore.Entities.ViewModels.Size;
using SiriusStyleRdStore.Utility.Extensions;

namespace SiriusStyleRdStoreApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly IItemService _itemService;

        public ProductController(IProductService productService, 
            IMapper mapper, IItemService itemService)
        {
            _productService = productService;
            _mapper = mapper;
            _itemService = itemService;
        }

        public async Task<IActionResult> Index()
        {
            var itemResponse = await _itemService
                .Get(new List<ItemType>
            {
                ItemType.Category,
                ItemType.Size
            });

            if (itemResponse is Success<ItemViewModel> item)
            {
                ViewBag.Categories = item.Response.Categories;
                ViewBag.Sizes = item.Response.Sizes;
            }

            return View();
        }

        public async Task<IActionResult> GetAll([DataSourceRequest] DataSourceRequest request)
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
        public async Task<IActionResult> BatchCreate([DataSourceRequest] DataSourceRequest request,
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
        public async Task<IActionResult> BatchUpdate([DataSourceRequest] DataSourceRequest request,
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

        public async Task<IActionResult> GetByOrderNumber(string orderNumber, [DataSourceRequest] DataSourceRequest request)
        {
            var response = await _productService.GetByOrderNumber(orderNumber).ConfigureAwait(false);

            if (response is Success<IEnumerable<ProductViewModel>> products)
                return Json(await products.Response.ToDataSourceResultAsync(request));

            throw new Exception();
        }

        public async Task<ActionResult> GetAllForOrderDetails([DataSourceRequest] DataSourceRequest request, string orderNumber)
        {
            var response = await _productService.GetAllForOrderDetails(orderNumber).ConfigureAwait(false);

            if (response is Success<IEnumerable<ProductViewModel>> products)
                return Json(await products.Response.ToDataSourceResultAsync(request));

            throw new Exception();
        }
    }
}