using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using SiriusStyleRdStore.BL.Services;
using SiriusStyleRdStore.Entities.Enums;
using SiriusStyleRdStore.Entities.Requests.Order;
using SiriusStyleRdStore.Entities.ViewModels;
using SiriusStyleRdStore.Entities.ViewModels.Item;
using SiriusStyleRdStore.Entities.ViewModels.Order;
using SiriusStyleRdStore.Entities.ViewModels.Product;

namespace SiriusStyleRdStoreApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IItemService _itemService;
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService, IMapper mapper,
            IItemService itemService)
        {
            _orderService = orderService;
            _mapper = mapper;
            _itemService = itemService;
        }

        public async Task<IActionResult> Index()
        {
            var itemResponse = await _itemService
                .Get(new List<ItemType>
                {
                    ItemType.Size
                });

            if (itemResponse is Success<ItemViewModel> item)
            {
                ViewBag.Sizes = item.Response.Sizes;
            }

            return View();
        }

        public async Task<IActionResult> Create()
        {
            var itemResponse = await _itemService
                .Get(new List<ItemType>
                {
                    ItemType.Customer,
                    ItemType.Size
                });

            if (itemResponse is Success<ItemViewModel> item)
            {
                ViewBag.Customers = item.Response.Customers;
                ViewBag.Sizes = item.Response.Sizes;
            }

            return View();
        }

        public async Task<JsonResult> GetAll([DataSourceRequest] DataSourceRequest request)
        {
            var response = await _orderService.GetAll().ConfigureAwait(false);

            if (response is Success<IEnumerable<OrderViewModel>> orders)
                return Json(await orders.Response.ToDataSourceResultAsync(request));

            throw new Exception();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(OrderRequest order)
        {
            if (order.OrderNumber.HasValue())
            {
                var _ = await _orderService.Update(_mapper.Map<UpdateOrderRequest>(order));

                return RedirectToAction(nameof(Index));
            }

            _ = await _orderService.Create(_mapper.Map<CreateOrderRequest>(order));

            return RedirectToAction(nameof(Index));
        }

        public async Task<JsonResult> AjaxUpsert(OrderRequest order)
        {
            if (order.OrderNumber.HasValue())
            {
                var _ = await _orderService.Update(_mapper.Map<UpdateOrderRequest>(order));

                return Json(order);
            }

            _ = await _orderService.Create(_mapper.Map<CreateOrderRequest>(order));

            //return RedirectToAction(nameof(Index));
            return Json(order);
        }
    }
}